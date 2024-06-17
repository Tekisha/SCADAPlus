using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.interfaces
{
    public interface IAlarmRepository
    {
        Task<IEnumerable<Alarm>> GetAll();
        Task<Alarm> Get(string alarmName);
        Task<IEnumerable<Alarm>> GetInvoked(string tagId, double value);
        Task<Alarm> Create(Alarm newAlarm);
        Task<Alarm> Delete(string alarmName);
        Task<Alarm> Update(Alarm updatedAlarm);
        Task<IEnumerable<Alarm>> GetByTagId(string tagId);
        Task<Alarm> SaveTriggeredAlarm(Alarm triggeredAlarm);
    }
}
