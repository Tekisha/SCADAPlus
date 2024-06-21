using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SCADA_Core.Models;

namespace SCADA_Core.Utilities;

public class ConfigManager
{
    private const string ConfigFilePath = "scadaConfig.xml";

    public static void SaveConfig(ConfigData configData)
    {
        try
        {
            using var writer = new StreamWriter(ConfigFilePath);
            var serializer = new XmlSerializer(typeof(ConfigData));
            serializer.Serialize(writer, configData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving config: {ex.Message}");
        }
    }

    public static ConfigData LoadConfig()
    {
        try
        {
            if (File.Exists(ConfigFilePath))
            {
                using var reader = new StreamReader(ConfigFilePath);
                var serializer = new XmlSerializer(typeof(ConfigData));
                return (ConfigData)serializer.Deserialize(reader);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading config: {ex.Message}");
        }

        return new ConfigData();
    }

    public static void ApplyConfigurationSettings(ConfigData configData)
    {
        foreach (var setting in configData.Settings)
        {
            Console.WriteLine($"Loaded setting: {setting.Key} = {setting.Value}");
            switch (setting.Key)
            {
                case "ScanInterval":
                {
                    if (int.TryParse(setting.Value, out var scanInterval))
                        Console.WriteLine($"Scan Interval set to {scanInterval} ms");
                    break;
                }
                case "LogLevel":
                    Console.WriteLine($"Log Level set to {setting.Value}");
                    break;
            }
        }

        if (configData.SimulationDriverConfig == null) return;
        Console.WriteLine($"Simulation Driver Sine Signal: {configData.SimulationDriverConfig.SineSignal}");
        Console.WriteLine($"Simulation Driver Cosine Signal: {configData.SimulationDriverConfig.CosineSignal}");
        Console.WriteLine($"Simulation Driver Ramp Signal: {configData.SimulationDriverConfig.RampSignal}");
    }

    [Serializable]
    public class ConfigData
    {
        public List<ConfigSetting> Settings { get; set; } = [];
        public SimulationDriverConfig SimulationDriverConfig { get; set; } = new();
        public TagSettings TagSettings { get; set; } = new();
        public List<Tag> Tags { get; set; } = [];
    }

    [Serializable]
    public class ConfigSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class SimulationDriverConfig
    {
        public string SineSignal { get; set; }
        public string CosineSignal { get; set; }
        public string RampSignal { get; set; }
    }

    [Serializable]
    public class TagSettings
    {
        public AnalogInputTagSettings AnalogInputTag { get; set; } = new();
        public DigitalInputTagSettings DigitalInputTag { get; set; } = new();
    }

    [Serializable]
    public class AnalogInputTagSettings
    {
        public int ScanTime { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Driver { get; set; }
    }

    [Serializable]
    public class DigitalInputTagSettings
    {
        public int ScanTime { get; set; }
        public string Driver { get; set; }
    }
}