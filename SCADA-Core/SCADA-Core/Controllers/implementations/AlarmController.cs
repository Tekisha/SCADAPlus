using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SCADA_Core.Controllers.implementations
{
    public class AlarmController : IAlarmController
    {
        private IAlarmService alarmService;
        private ITagService tagService;

        public delegate void AlarmTriggered(TriggeredAlarmDto triggeredAlarmDto);
        public event AlarmTriggered OnAlarmTriggered;


        public AlarmController(IAlarmService alarmService, ITagService tagService)
        {
            this.alarmService = alarmService;
            this.tagService = tagService;
            this.alarmService.Subscribe(HandleAlarm);
        }

        public IEnumerable<AlarmDto> GetAll()
        {
            return alarmService.GetAll();
        }

        public void Subscribe()
        {
            OnAlarmTriggered += OperationContext.Current.GetCallbackChannel<IAlarmControllerCallback>().OnAlarmTriggered;
        }

        private void HandleAlarm(Alarm alarm)
        {
            if (OnAlarmTriggered == null)
            {
                return;
            }

            TriggeredAlarmDto triggeredAlarmDto = new TriggeredAlarmDto
            {
                Alarm = alarm,
                TagDescription = tagService.GetTag(alarm.TagId).Description
            };

            foreach(AlarmTriggered handler in OnAlarmTriggered.GetInvocationList().Cast<AlarmTriggered>())
            {
                try
                {
                    handler(triggeredAlarmDto);
                } catch (TimeoutException)
                {
                    Console.WriteLine("Alarm handler timed out. Removing it.");
                    OnAlarmTriggered -= handler;
                }
            }

        }
    }
}
