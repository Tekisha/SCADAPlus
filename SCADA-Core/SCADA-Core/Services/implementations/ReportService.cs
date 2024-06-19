using SCADA_Core.Enums;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Services.implementations
{
    public class ReportService(ITagValueRepository tagValueRepository, IAlarmRepository alarmRepository) : IReportService
    {
        public List<Alarm> GetAlarmsByPriority(AlarmPriority priority)
        {
            return alarmRepository.GetTriggeredAlarmsByPriority(priority);
        }

        public List<Alarm> GetAlarmsDuringInterval(DateTime start, DateTime end)
        {
            return alarmRepository.GetTriggeredAlarmsDuringInterval(start, end);
        }

        public List<TagValue> GetAllTagValues(string id)
        {
            return tagValueRepository.GetAllTagValues(id);
        }

        public List<TagValue> GetLatestAnalogInputTagValues()
        {
            return tagValueRepository.GetLatestAnalogInputTagValues();
        }

        public List<TagValue> GetLatetstDigitalInputTagValues()
        {
            return tagValueRepository.GetLatetstDigitalInputTagValues();
        }

        public List<TagValue> GetTagValuesDuringInterval(DateTime start, DateTime end)
        {
            return tagValueRepository.GetTagValuesDuringInterval(start, end);
        }
    }
}
