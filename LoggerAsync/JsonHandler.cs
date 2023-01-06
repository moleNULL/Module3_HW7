using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoggerAsync
{
    internal static class JsonHandler
    {
        private static readonly string _appsettingsFile = "appsettings.json";
        private static AppSettingsJson? _settings;

        static JsonHandler()
        {
            string json = File.ReadAllText(_appsettingsFile);
            _settings = JsonSerializer.Deserialize<AppSettingsJson>(json);
        }

        public static string GetLogFilePath()
        {
            if (_settings is null)
            {
                throw new ApplicationException($"Failed to deserialize {_appsettingsFile}");
            }

            if (string.IsNullOrEmpty(_settings.FileName))
            {
                throw new Exception("Exception! logFileName is null or empty");
            }

            return _settings.FileName;
        }

        public static int GetLogsNumber()
        {
            if (_settings is null)
            {
                throw new ApplicationException($"Failed to deserialize {_appsettingsFile}");
            }

            return _settings.LogsNumber;
        }

        // The only purpose of having this class is to deserialize json into an object
        private class AppSettingsJson
        {
            [JsonPropertyName("logFileName")]
            public string? FileName { get; set; }

            [JsonPropertyName("logsNumberBeforeBackup")]
            public int LogsNumber { get; set; }
        }
    }
}
