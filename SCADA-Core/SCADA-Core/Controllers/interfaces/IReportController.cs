using SCADA_Core.DTOs;
using SCADA_Core.Enums;
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
    public interface IReportController
    {
        [OperationContract]
        List<Alarm> GetAlarmsDuringInterval(DateTime start, DateTime end, string token);

        [OperationContract]
        List<Alarm> GetAlarmsByPriority(AlarmPriority priority, string token);

        [OperationContract]
        List<TagValueDTO> GetTagValuesDuringInterval(DateTime start, DateTime end, string token);

        [OperationContract]
        List<TagValueDTO> GetLatestAnalogInputTagValues(string token);

        [OperationContract]
        List<TagValueDTO> GetLatetstDigitalInputTagValues(string token);

        [OperationContract]
        List<TagValueDTO> GetAllTagValues(string id, string token);
    }
}
