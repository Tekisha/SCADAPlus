using SCADA_Core.Enums;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Services.interfaces
{
    public interface IReportService
    {

        List<Alarm> GetAlarmsDuringInterval(DateTime start, DateTime end);

        List<Alarm> GetAlarmsByPriority(AlarmPriority priority);

        List<TagValue> GetTagValuesDuringInterval(DateTime start, DateTime end);

        List<TagValue> GetLatestAnalogInputTagValues();

        List<TagValue> GetLatetstDigitalInputTagValues();

        List<TagValue> GetAllTagValues(string id);
    }
}
