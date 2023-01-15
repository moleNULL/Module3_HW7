namespace LoggerAsync
{
    internal class Starter
    {
        public static async Task RunAsync()
        {
            var logger = new Logger();
            int recordNumber = 50;

            Console.WriteLine($"\t\tWrite {recordNumber} records into the log file by each of two methods ({recordNumber} * 2 = {recordNumber * 2} records)\n\n");

            var tasks = new List<Task>(recordNumber);

            for (int i = 0; i < recordNumber; i++)
            {
                var t1 = logger.WriteLog1();
                var t2 = logger.WriteLog2();

                // imitate a lasting task
                await Task.Delay(333);

                tasks.Add(t1);
                tasks.Add(t2);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("All logs have been successfully written into the provided log file");
        }

        // Method subscribed to Logger.Notify event
        public static void BackupRequest(int backupNumber, int logsNumber)
        {
            Console.WriteLine($"\nMaking backup #{backupNumber}: {logsNumber} logs\n");

            Backup.CreateBackup();
        }
    }
}
