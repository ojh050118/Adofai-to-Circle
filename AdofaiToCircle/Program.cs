using System.Diagnostics;
using System.IO.Compression;
using AdofaiToCircle.Adofai;
using AdofaiToCircle.Circle;
using AdofaiToCircle.Convert;
using AdofaiToCircle.Convert.Circle;
using AdofaiToCircle.Extensions;
using AdofaiToCircle.IO;
using Newtonsoft.Json;

namespace AdofaiToCircle
{
    public class Program
    {
        public static string RunningDirectory => Directory.GetCurrentDirectory();

        private const string import_folder = @"Import";
        private const string export_folder = @"Export";

        private static DirectoryManager importDir = new DirectoryManager(Path.Combine(RunningDirectory, import_folder));
        private static DirectoryManager exportDir = new DirectoryManager(Path.Combine(RunningDirectory, export_folder));

        private static AdofaiFileReader adofaiFileReader = new AdofaiFileReader();
        private static BeatmapConverter<AdofaiFile, CircleFile> converter = new CircleBeatmapConverter();

        private static readonly Stopwatch stopwatch = new Stopwatch();

        public static void Main()
        {
            // 파일 중복 오류를 막기 위해 폴더를 정리합니다.
            exportDir.DeleteAllContents();

            Console.WriteLine($"Ready to convert {importDir.GetDirectories().Length} adofai levels!");
            Console.ReadLine();

            stopwatch.Start();

            foreach (var adofaiDir in importDir.GetDirectories())
            {
                foreach (var adofaiFile in adofaiDir.GetFiles("*.adofai"))
                {
                    // backup.adofai는 건너뛰겠습니다.
                    if (adofaiFile.Name == "backup.adofai")
                        continue;

                    AdofaiFile adofai = null;
                    try
                    {
                        write($"Loading \"{adofaiFile.Directory?.Name}/{adofaiFile.Name}\"...");
                        adofai = adofaiFileReader.Get(adofaiFile.FullName);
                    }
                    catch (Exception e)
                    {
                        error($"{e.Message}");
                        continue;
                    }

                    // OS에서 허용되는 파일문자로 대체합니다.
                    var fileName = FileExtensions.ReplaceSafeChar($"[{adofai.Settings?.Author}] {adofai.Settings?.Artist} - {adofai.Settings?.Song}.circle");

                    // adofai 에서 우리가 원하는 형식으로 변환을 합니다.
                    var circle = converter.Convert(adofai);

                    // 비트맵 폴더를 만듭니다.
                    var beatmapDir = new DirectoryManager(Path.Combine(RunningDirectory, export_folder, Path.GetFileNameWithoutExtension(fileName)));
                    try
                    {
                        write($"Writing to \"{fileName}\"...");
                        using (StreamWriter sw = File.CreateText(Path.Combine(beatmapDir.FullName, fileName)))
                            sw.WriteLine(JsonConvert.SerializeObject(circle));
                    }
                    catch (Exception e)
                    {
                        error($"Error while writing {Path.GetFileNameWithoutExtension(fileName)}: {e.Message}");
                    }

                    write($"Copying beatmap resources...");
                    FileExtensions.TryCopy(Path.Combine(adofaiDir.FullName, circle.Settings.BgImage), Path.Combine(beatmapDir.FullName, circle.Settings.BgImage));
                    FileExtensions.TryCopy(Path.Combine(adofaiDir.FullName, circle.Settings.SongFileName), Path.Combine(beatmapDir.FullName, circle.Settings.SongFileName));
                    FileExtensions.TryCopy(Path.Combine(adofaiDir.FullName, circle.Settings.BgVideo), Path.Combine(beatmapDir.FullName, circle.Settings.BgVideo));

                    try
                    {
                        write($"Compressing to \"{Path.GetFileNameWithoutExtension(fileName)}.circlez\"...\n");
                        ZipFile.CreateFromDirectory(beatmapDir.FullName, $"{beatmapDir.FullName}.circlez");
                    }
                    catch
                    {
                        error($"Error while compressing.");
                    }
                }
            }

            // 모든 변환이 끝났으니 압축에 사용된 폴더들을 정리합니다.
            foreach (var di in exportDir.GetDirectories())
                di.Delete(true);

            write($"Converted {exportDir.GetFiles("*.circlez").Length} levels!\n");
            Console.ReadLine();
        }

        private static void write(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            if (stopwatch.ElapsedMilliseconds > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(stopwatch.ElapsedMilliseconds.ToString().PadRight(8));
            }

            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }

        private static void error(string message, bool mustStop = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (mustStop)
            {
                Console.WriteLine("Fatal error:".PadRight(8) + message);
                Console.ReadLine();
                Environment.Exit(-1);
            }
            else
            {
                Console.WriteLine("Error:".PadRight(8) + message);
                Console.ResetColor();
            }
        }
    }
}
