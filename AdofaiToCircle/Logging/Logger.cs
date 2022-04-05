namespace AdofaiToCircle.Logging
{
    public class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Error(Exception e, string description)
        {
            Console.WriteLine(e.Message + description);
        }
    }
}
