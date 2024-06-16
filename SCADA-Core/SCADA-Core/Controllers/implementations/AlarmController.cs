using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.implementations
{
    public class AlarmController(IAlarmService alarmService) : IAlarmController
    {
        public IEnumerable<AlarmDto> GetAll()
        {
            return alarmService.GetAll();
        }

        public AlarmDto Get(string alarmName)
        {
            return alarmService.Get(alarmName);
        }

        public IEnumerable<AlarmDto> GetInvoked(string tagId, double value)
        {
            return alarmService.GetInvoked(tagId, value);
        }

        public AlarmDto Create(AlarmDto newAlarm)
        {
            return alarmService.Create(newAlarm);
        }

        public AlarmDto Delete(string alarmName)
        {
            return alarmService.Delete(alarmName);
        }

        public AlarmDto Update(AlarmDto updatedAlarm)
        {
            return alarmService.Update(updatedAlarm);
        }

        public IEnumerable<AlarmDto> GetByTag(string tagId)
        {
            return alarmService.GetByTag(tagId);
        }
    }
}
