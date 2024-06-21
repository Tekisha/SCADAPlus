using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Controllers.implementations;

public class AlarmController : IAlarmController
{
    public delegate void AlarmTriggered(TriggeredAlarmDto triggeredAlarmDto);

    private readonly IAlarmService _alarmService;
    private readonly ITagService _tagService;


    public AlarmController(IAlarmService alarmService, ITagService tagService)
    {
        _alarmService = alarmService;
        _tagService = tagService;
        _alarmService.Subscribe(HandleAlarm);
    }

    public IEnumerable<AlarmDto> GetAll()
    {
        return _alarmService.GetAll();
    }

    public void Subscribe()
    {
        OnAlarmTriggered += OperationContext.Current.GetCallbackChannel<IAlarmControllerCallback>().OnAlarmTriggered;
    }

    public event AlarmTriggered OnAlarmTriggered;

    private void HandleAlarm(Alarm alarm)
    {
        if (OnAlarmTriggered == null) return;

        var triggeredAlarmDto = new TriggeredAlarmDto
        {
            Alarm = alarm,
            TagDescription = _tagService.GetTag(alarm.TagId).Description
        };

        foreach (var handler in OnAlarmTriggered.GetInvocationList().Cast<AlarmTriggered>())
            try
            {
                handler(triggeredAlarmDto);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Alarm handler timed out. Removing it.");
                OnAlarmTriggered -= handler;
            }
    }
}