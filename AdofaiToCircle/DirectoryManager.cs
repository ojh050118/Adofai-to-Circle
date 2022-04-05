namespace AdofaiToCircle
{
    public class DirectoryManager
    {
        private DirectoryInfo directoryInfo;

        public string ImportDirectoryName;

        public string FullName => directoryInfo.FullName;

        public string Name => directoryInfo.Name;

        /// <summary>
        /// 특정 경로의 폴더 관리자의 인스턴스를 생성합니다.
        /// 경로가 존재하지 않으면 생성합니다.
        /// </summary>
        /// <param name="path">관리할 폴더의 절대경로.</param>
        public DirectoryManager(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            directoryInfo = new DirectoryInfo(path);
        }

        public FileInfo[] GetFiles() => directoryInfo.GetFiles();
        public FileInfo[] GetFiles(string searchPattern) => directoryInfo.GetFiles(searchPattern);
        public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption) => directoryInfo.GetFiles(searchPattern, searchOption);

        public DirectoryInfo[] GetDirectories() => directoryInfo.GetDirectories();

        public void Copy(string sourceFileName) => File.Copy(sourceFileName, Path.Combine(directoryInfo.FullName, Path.GetFileName(sourceFileName)));

        public bool Exists(string path) => File.Exists(Path.Combine(directoryInfo.FullName, path));

        public void Delete(bool recursive = true) => directoryInfo.Delete(recursive);

        public void DeleteAllContents()
        {
            foreach (var fi in GetFiles())
                File.Delete(fi.FullName);

            foreach (var di in GetDirectories())
                Directory.Delete(di.FullName, true);
        }
    }
}
