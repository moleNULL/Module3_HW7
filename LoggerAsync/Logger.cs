namespace LoggerAsync
{
    internal class Logger
    {
        private string _logFilePath;
        private int _logsStepNumber; // every step is a number of logs after which you need to backup
        private int _logsNumberBeforeBackup; // number of logs before backup invoked
        private Mutex _mutex; // used in WriteToFile() to seperate log.txt from threads

        private int _counter; // needed to count the number of a current log written into file and console
        private int _backupNumber; // needed to count how many backup are made to pass it to Starter.BackupRequest()

        public Logger()
        {
            _logFilePath = JsonHandler.GetLogFilePath();

            _logsStepNumber = JsonHandler.GetLogsNumber();
            _logsNumberBeforeBackup = GetFirtLogNumberToBackup();
            _mutex = new Mutex();

            _counter = GetLastRecordNumber();
            _backupNumber = 0;

            Notify += Starter.BackupRequest;
        }

        public event Action<int, int> Notify;

        // First method that writes into log.txt
        public async Task WriteLog1()
        {
            string log = $"{{{DateTime.Now}}}: {{Warning}}:\t{{Skipped logic in method #{LoggerRandomizer.RandomNumber} after delay in {LoggerRandomizer.RandomDelay} ms}}\n";

            await Task.Delay(LoggerRandomizer.RandomDelay); // imitate a lasting task
            WriteToFile(log);
        }

        // Second method that writes into log.txt
        public async Task WriteLog2()
        {
            string log = $"{{{DateTime.Now}}}: {{Info}}:\t{{Started method #{LoggerRandomizer.RandomNumber} after delay in {LoggerRandomizer.RandomDelay} ms}}\n";

            await Task.Delay(LoggerRandomizer.RandomDelay); // imitate a lasting task
            WriteToFile(log);
        }

        // Method that actually writes to log.txt
        private void WriteToFile(string log)
        {
            _mutex.WaitOne();

            log = ++_counter + ": " + log;
            File.AppendAllText(_logFilePath, log);
            Console.WriteLine(log);

            CheckIfBackupNeeded();

            _mutex.ReleaseMutex();
        }

        // Get the next number of records in log.txt to perform backup
        private int GetFirtLogNumberToBackup()
        {
            if (!File.Exists(_logFilePath))
            {
                return _logsStepNumber;
            }

            string[] lines = File.ReadAllLines(_logFilePath);

            if (lines.Length > _logsStepNumber)
            {
                // example: ((56 / 10) + 1) * 10 = 60 --> on 60th record perform backup
                _logsNumberBeforeBackup = ((lines.Length / _logsStepNumber) + 1) * _logsStepNumber;
            }

            // if there is no records in log.txt set *_logsStepNumber* as default
            return _logsNumberBeforeBackup == 0 ? _logsStepNumber : _logsNumberBeforeBackup;
        }

        // Get the latest number of a record in log.txt
        private int GetLastRecordNumber()
        {
            if (!File.Exists(_logFilePath))
            {
                return 0;
            }

            return File.ReadAllLines(_logFilePath).Length;
        }

        // Check if there is already enough records in log.txt to perform backup
        private void CheckIfBackupNeeded()
        {
            string[] lines = File.ReadAllLines(_logFilePath);

            if (_logsNumberBeforeBackup == lines.Length)
            {
                Notify.Invoke(++_backupNumber, _logsNumberBeforeBackup);

                _logsNumberBeforeBackup += _logsStepNumber;
            }
        }
    }
}
