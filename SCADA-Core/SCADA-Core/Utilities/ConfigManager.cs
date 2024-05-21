using SCADA_Core.Repositories.implementations;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SCADA_Core.Utilities
{
    public class ConfigManager
    {
        private const string ConfigFilePath = "scadaConfig.xml";

        public static void SaveConfig(UserRepository userRepository, TagRepository tagRepository)
        {
            try
            {
                using (var writer = new StreamWriter(ConfigFilePath))
                {
                    var serializer = new XmlSerializer(typeof(ConfigData));
                    var configData = new ConfigData
                    {
                        Users = (List<User>)userRepository.GetAllUsers(),
                        Tags = (List<Tag>)tagRepository.GetAllTags()
                    };
                    serializer.Serialize(writer, configData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving config: {ex.Message}");
            }
        }

        public static (UserRepository, TagRepository) LoadConfig()
        {
            var userRepository = new UserRepository();
            var tagRepository = new TagRepository();
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    using (var reader = new StreamReader(ConfigFilePath))
                    {
                        var serializer = new XmlSerializer(typeof(ConfigData));
                        var configData = (ConfigData)serializer.Deserialize(reader);
                        foreach (var user in configData.Users)
                        {
                            userRepository.RegisterUser(user);
                        }
                        foreach (var tag in configData.Tags)
                        {
                            tagRepository.AddTag(tag);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading config: {ex.Message}");
            }

            return (userRepository, tagRepository);
        }

        [Serializable]
        public class ConfigData
        {
            public List<User> Users { get; set; }
            public List<Tag> Tags { get; set; }
        }
    }
}
