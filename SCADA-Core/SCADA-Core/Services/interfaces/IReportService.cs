using System;
using System.Collections.Generic;
using SCADA_Core.Enums;
using SCADA_Core.Models;

namespace SCADA_Core.Services.interfaces;

public interface IReportService
{
    List<Alarm> GetAlarmsDuringInterval(DateTime start, DateTime end);

    List<Alarm> GetAlarmsByPriority(AlarmPriority priority);

    List<TagValue> GetTagValuesDuringInterval(DateTime start, DateTime end);

    List<TagValue> GetLatestAnalogInputTagValues();

    List<TagValue> GetLatetstDigitalInputTagValues();

    List<TagValue> GetAllTagValues(string id);
}