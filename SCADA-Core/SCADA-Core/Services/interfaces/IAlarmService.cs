using SCADA_Core.DTOs;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SCADA_Core.Services.implementations.AlarmService;

namespace SCADA_Core.Services.interfaces
{
    public interface IAlarmService
    {
        IEnumerable<AlarmDto> GetAll();
        AlarmDto Get(string alarmName);
        IEnumerable<AlarmDto> GetInvoked(string tagId, double value);
        AlarmDto Create(AlarmDto newAlarm);
        AlarmDto Delete(string alarmName);
        AlarmDto Update(AlarmDto updatedAlarm);
        IEnumerable<AlarmDto> GetByTag(string tagId);
        void HandleTriggeredAlarm(Alarm alarm);
        void Subscribe(AlarmTriggered alarmTriggeredDelegate);
    }
}
