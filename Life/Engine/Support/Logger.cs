using System;
using System.IO;

namespace Life.Engine.Support
{
    public class Logger
    {
        public static void Log(string message)
        {
            File.AppendAllText(GetLogFile("Log"), BuildLog(message));
        }

        public static void ErrorLog(Exception e)
        {
            File.AppendAllText(GetLogFile("ErrorLog"), BuildLog(e.Message + "\n\nStackTrace:\n" + e.StackTrace));
        }

        public static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            ErrorLog((Exception)args.ExceptionObject);
        }

        private static string GetLogFile(string fileName)
        {
            uint fileNumb = (uint)Directory.GetFiles(AppContext.BaseDirectory, $"{fileName}*.txt", SearchOption.TopDirectoryOnly).Length;

            FileInfo logFile = new FileInfo($"{fileName}{fileNumb}.txt");

            if (fileNumb == 0 || logFile.Length > 1048576) 
            {
                fileNumb++;
                logFile = new FileInfo($"{fileName}{fileNumb}.txt");
                logFile.Create().Close();   
            }

            return logFile.FullName;
        }

        private static string BuildLog(string text)
        {
            string log = "";
            log += "---------------------------------------------------------------------------------------";
            log += "\nTime: " + System.DateTime.Now.ToString();
            log += "\nMessage:\n";
            log += text;
            log += "\n---------------------------------------------------------------------------------------\n";
            return log;
        }
    }
}
