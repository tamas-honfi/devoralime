using Newtonsoft.Json;
using NLog;

namespace ToBattle.Services
{
    public class Setting
    {
        [JsonProperty("useSeed")] public bool UseSeed { get; set; } = true;
        [JsonProperty("seed")] public int Seed { get; set; } = 32768;

        [JsonProperty("heroesCount")] public int HeroesCount { get; set; } = 10;
    }

    public class SettingsService
    {
        private readonly string _fileName = "config.json";
        private readonly ILogger _logger;

        public SettingsService(ILogger logger)
        {
            _logger = logger;
        }

        public Setting LoadSettings()
        {
            Setting? setting = null;
            if (!File.Exists(_fileName))
            {
                _logger.Warn($"Config file not found, creating a new one with default settings.");

                // create default setting & save
                setting = new Setting();
                SaveSettings(setting);
            }

            string json  = File.ReadAllText(_fileName);
            setting = JsonConvert.DeserializeObject<Setting>(json);
            if (setting == null)
            {
                _logger.Error($"Could not deserialize settings, returning defaults.");
                setting = new Setting();
            }

            return setting;
        }

        public void SaveSettings(Setting setting)
        {
            string json = JsonConvert.SerializeObject(setting);
            File.WriteAllText(_fileName, json);
        }
    }
}
