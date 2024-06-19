using System.Collections.Generic;
using System.ServiceModel;
using SCADA_Core.DTOs;

namespace SCADA_Core.Controllers.interfaces;

[ServiceContract]
public interface ITagController
{
    [OperationContract]
    double GetTagValue(string address, string token);

    [OperationContract]
    void AddTag(TagDto tagDto, string token);

    [OperationContract]
    void RemoveTag(string id, string token);

    [OperationContract]
    void ChangeOutputValue(string tagId, double newValue, string token);

    [OperationContract]
    double GetOutputValue(string tagId, string token);

    [OperationContract]
    void TurnScanOnOff(string tagId, bool onOff, string token);

    [OperationContract]
    List<BaseTagInfoDto> GetAllTags(string token);

    [OperationContract]
    AlarmDto GetAlarm(string alarmName, string token);

    [OperationContract]
    IEnumerable<AlarmDto> GetInvokedAlarms(string tagId, double value, string token);

    [OperationContract]
    AlarmDto CreateAlarm(AlarmDto newAlarm, string token);

    [OperationContract]
    AlarmDto DeleteAlarm(string alarmName, string token);

    [OperationContract]
    AlarmDto UpdateAlarm(AlarmDto updatedAlarm, string token);

    [OperationContract]
    IEnumerable<AlarmDto> GetAlarmsByTag(string tagId, string token);

    [OperationContract]
    IEnumerable<AlarmDto> GetAllAlarms(string token);
}