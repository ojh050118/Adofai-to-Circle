namespace AdofaiToCircle.Logging
{
    // Todo: yyyy-mm-dd-hh-mm-ss.log 파일에 로그 저장 기능 추가하기.
    public class Logger
    {
        // Todo: 로그 수준(상세한 정도) 추가
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        // Todo: 로그 수준(상세한 정도) 추가
        public static void Error(Exception e, string description)
        {
            Console.WriteLine(e.Message + description);
        }
    }
}
