using System.Collections.Generic;
using System.ServiceModel;
using SCADA_Core.DTOs;

namespace SCADA_Core.Controllers.interfaces;

[ServiceContract(CallbackContract = typeof(IAlarmControllerCallback))]
public interface IAlarmController
{
    [OperationContract]
    IEnumerable<AlarmDto> GetAll();

    [OperationContract]
    void Subscribe();
}

[ServiceContract]
public interface IAlarmControllerCallback
{
    [OperationContract(IsOneWay = true)]
    void OnAlarmTriggered(TriggeredAlarmDto triggeredAlarmDto);
}