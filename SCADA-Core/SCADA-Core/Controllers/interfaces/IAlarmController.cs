using SCADA_Core.DTOs;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.interfaces
{
    [ServiceContract]
    public interface IAlarmController
    {
        [OperationContract]
        IEnumerable<AlarmDto> GetAll();

        [OperationContract]
        AlarmDto Get(string alarmName);

        [OperationContract]
        IEnumerable<AlarmDto> GetInvoked(string tagId, double value);

        [OperationContract]
        AlarmDto Create(AlarmDto newAlarm);

        [OperationContract]
        AlarmDto Delete(string alarmName);

        [OperationContract]
        AlarmDto Update(AlarmDto updatedAlarm);

        [OperationContract]
        IEnumerable<AlarmDto> GetByTag(string tagId);
    }
}
