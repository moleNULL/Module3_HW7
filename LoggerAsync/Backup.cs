namespace LoggerAsync
{
    internal static class Backup
    {
        private static string _logFilePath;

        static Backup()
        {
            if (!Directory.Exists("Backup"))
            {
                Directory.CreateDirectory("Backup");
            }

            _logFilePath = JsonHandler.GetLogFilePath();
        }

        public static void CreateBackup()
        {
            // file sample: 06012023_150729_log.txt
            string fileName = "Backup\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "_log.txt";
            File.Copy(_logFilePath, fileName);
        }
    }
}
