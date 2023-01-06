namespace LoggerAsync
{
    internal static class Backup
    {
        private static string _logFilePath;
        private static string _backupFolder;

        static Backup()
        {
            _logFilePath = JsonHandler.GetLogFilePath();
            _backupFolder = "Backup";

            if (!Directory.Exists(_backupFolder))
            {
                Directory.CreateDirectory(_backupFolder);
            }
        }

        public static void CreateBackup()
        {
            // file sample: 06012023_150729_log.txt
            string fileName = $"{_backupFolder}\\{DateTime.Now.ToString("ddMMyyyy_HHmmss")}_log.txt";
            File.Copy(_logFilePath, fileName);
        }
    }
}
