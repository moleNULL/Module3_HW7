namespace LoggerAsync
{
    internal static class LoggerRandomizer
    {
        private static Random _random;

        static LoggerRandomizer()
        {
            _random = new Random();
        }

        public static int RandomNumber => _random.Next(100 + 1);
        public static int RandomDelay => _random.Next(500, 3000 + 1);
    }
}
