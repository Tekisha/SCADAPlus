using SCADA_Core.Enums;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.implementations
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly List<Alarm> alarms = new List<Alarm>();

        public Task<IEnumerable<Alarm>> GetAll()
        {
            return Task.FromResult(alarms.AsEnumerable());
        }

        public Task<Alarm> Get(string alarmName)
        {
            var alarm = alarms.FirstOrDefault(a => a.AlarmName == alarmName);
            return Task.FromResult(alarm);
        }

        public Task<IEnumerable<Alarm>> GetInvoked(string tagId, double value)
        {
            var invokedAlarms = alarms.Where(a => a.TagId == tagId &&
                                                   ((a.Type == AlarmType.Above && value > a.Limit) ||
                                                    (a.Type == AlarmType.Below && value < a.Limit)));
            return Task.FromResult(invokedAlarms.AsEnumerable());
        }

        public Task<Alarm> Create(Alarm newAlarm)
        {
            alarms.Add(newAlarm);
            return Task.FromResult(newAlarm);
        }

        public Task<Alarm> Delete(string alarmName)
        {
            var alarm = alarms.FirstOrDefault(a => a.AlarmName == alarmName);
            if (alarm != null)
            {
                alarms.Remove(alarm);
            }
            return Task.FromResult(alarm);
        }

        public Task<Alarm> Update(Alarm updatedAlarm)
        {
            var alarm = alarms.FirstOrDefault(a => a.AlarmName == updatedAlarm.AlarmName);
            if (alarm != null)
            {
                alarms.Remove(alarm);
                alarms.Add(updatedAlarm);
            }
            return Task.FromResult(updatedAlarm);
        }

        public Task<IEnumerable<Alarm>> GetByTagId(string tagId)
        {
            var tagAlarms = alarms.Where(a => a.TagId == tagId);
            return Task.FromResult(tagAlarms.AsEnumerable());
        }
    }
}
