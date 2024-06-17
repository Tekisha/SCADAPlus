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
    }
}
