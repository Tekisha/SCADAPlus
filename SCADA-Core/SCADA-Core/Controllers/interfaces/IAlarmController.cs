using SCADA_Core.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace SCADA_Core.Controllers.interfaces
{
    [ServiceContract(CallbackContract = typeof(IAlarmControllerCallback))]
    public interface IAlarmController
    {
        [OperationContract]
        IEnumerable<AlarmDto> GetAll();
        [OperationContract]
        void Subscribe();
    }
    public interface IAlarmControllerCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnAlarmTriggered(TriggeredAlarmDto triggeredAlarmDto);
    }
}
