using Org.BouncyCastle.Security;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SCADA_Core.Services.implementations
{
    public class AlarmService : IAlarmService
    {

        private readonly IAlarmRepository repository;
        private readonly ITagService tagService;
        private readonly string configFilePath = "alarmConfig.xml";
        private readonly string logFilePath = "alarmsLog.txt";

        public AlarmService(IAlarmRepository repository, ITagService tagService)
        {
            this.repository = repository;
            this.tagService = tagService;
            LoadAlarmsFromConfig();
        }

        public IEnumerable<AlarmDto> GetAll()
        {
            var alarms = repository.GetAll().Result;
            return alarms.Select(a => a.ToDto());
        }

        public AlarmDto Get(string alarmName)
        {
            ValidationService.ValidateEmptyString("alarmName", alarmName);
            var alarm = repository.Get(alarmName).Result;
            if (alarm == null)
            {
                throw new ObjectNotFoundException($"Alarm with name '{alarmName}' not found.");
            }
            return alarm.ToDto();
        }

        public IEnumerable<AlarmDto> GetInvoked(string tagName, double value)
        {
            var alarms = repository.GetInvoked(tagName, value).Result;
            return alarms.Select(a => a.ToDto());
        }

        public AlarmDto Create(AlarmDto newAlarm)
        {
            ValidationService.ValidateAlarm(newAlarm);
            var existingAlarm = repository.Get(newAlarm.AlarmName).Result;
            if (existingAlarm != null)
            {
                throw new InvalidOperationException($"Alarm with name '{newAlarm.AlarmName}' already exists.");
            }
            var tag = tagService.GetTag(newAlarm.TagId);
            if (!(tag is AnalogInputTag analogTag))
            {
                throw new InvalidOperationException("Alarm can be added only to analog tags.");
            }
            if (!(newAlarm.Limit < analogTag.HighLimit && newAlarm.Limit > analogTag.LowLimit))
            {
                throw new ArgumentException("Limit is not between high and low limit.");
            }
            var alarm = newAlarm.ToEntity();
            var createdAlarm = repository.Create(alarm).Result;
            SaveAlarmsToConfig();
            return createdAlarm.ToDto();
        }

        public AlarmDto Delete(string alarmName)
        {
            ValidationService.ValidateEmptyString("alarmName", alarmName);
            var alarm = repository.Delete(alarmName).Result;
            if (alarm == null)
            {
                throw new ObjectNotFoundException($"Alarm with name '{alarmName}' not found.");
            }
            SaveAlarmsToConfig();
            return alarm.ToDto();
        }

        public AlarmDto Update(AlarmDto updatedAlarm)
        {
            ValidationService.ValidateAlarm(updatedAlarm);
            var existingAlarm = repository.Get(updatedAlarm.AlarmName).Result;
            if (existingAlarm == null)
            {
                throw new ObjectNotFoundException($"Alarm with name '{updatedAlarm.AlarmName}' not found.");
            }
            var updated = repository.Update(updatedAlarm.ToEntity()).Result;
            SaveAlarmsToConfig();
            return updated.ToDto();
        }

        public IEnumerable<AlarmDto> GetByTag(string tagId)
        {
            var tag = tagService.GetTag(tagId);
            var alarms = repository.GetByTagId(tagId).Result;
            return alarms.Select(a => a.ToDto());
        }

        private void SaveAlarmsToConfig()
        {
            var alarms = repository.GetAll().Result;
            var xmlSerializer = new XmlSerializer(typeof(List<Alarm>));
            using (var writer = new StreamWriter(configFilePath))
            {
                xmlSerializer.Serialize(writer, alarms.ToList());
            }
        }

        private void LoadAlarmsFromConfig()
        {
            if (!File.Exists(configFilePath)) return;

            var xmlSerializer = new XmlSerializer(typeof(List<Alarm>));
            using (var reader = new StreamReader(configFilePath))
            {
                var alarms = (List<Alarm>)xmlSerializer.Deserialize(reader);
                foreach (var alarm in alarms)
                {
                    repository.Create(alarm).Wait();
                }
            }
        }

        public void LogAlarm(Alarm alarm)
        {
            using (var writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {alarm.AlarmName} (Priority: {alarm.Priority}, Type: {alarm.Type}, Limit: {alarm.Limit})");
            }
        }
    }


}
