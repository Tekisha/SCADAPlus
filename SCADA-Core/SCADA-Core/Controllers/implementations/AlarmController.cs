using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.implementations
{
    public class AlarmController(IAlarmService alarmService) : IAlarmController
    {
        public IEnumerable<AlarmDto> GetAll()
        {
            return alarmService.GetAll();
        }
    }
}
