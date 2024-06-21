using System;
using System.Collections.Generic;
using System.ServiceModel;
using SCADA_Core.DTOs;
using SCADA_Core.Enums;

namespace SCADA_Core.Controllers.interfaces;

[ServiceContract]
public interface IReportController
{
    [OperationContract]
    List<TriggeredAlarmDto> GetAlarmsDuringInterval(DateTime start, DateTime end, string token);

    [OperationContract]
    List<TriggeredAlarmDto> GetAlarmsByPriority(AlarmPriority priority, string token);

    [OperationContract]
    List<TagValueDto> GetTagValuesDuringInterval(DateTime start, DateTime end, string token);

    [OperationContract]
    List<TagValueDto> GetLatestAnalogInputTagValues(string token);

    [OperationContract]
    List<TagValueDto> GetLatetstDigitalInputTagValues(string token);

    [OperationContract]
    List<TagValueDto> GetAllTagValues(string id, string token);
}