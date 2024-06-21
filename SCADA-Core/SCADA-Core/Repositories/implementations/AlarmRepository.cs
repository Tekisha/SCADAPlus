using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCADA_Core.Enums;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;

namespace SCADA_Core.Repositories.implementations;

public class AlarmRepository(ScadaDbContext dbContext) : IAlarmRepository
{
    private readonly List<Alarm> _alarms = [];

    public Task<IEnumerable<Alarm>> GetAll()
    {
        return Task.FromResult(_alarms.AsEnumerable());
    }

    public Task<Alarm> Get(string alarmName)
    {
        var alarm = _alarms.FirstOrDefault(a => a.AlarmName == alarmName);
        return Task.FromResult(alarm);
    }

    public Task<IEnumerable<Alarm>> GetInvoked(string tagId, double value)
    {
        var invokedAlarms = _alarms.Where(a => a.TagId == tagId &&
                                               ((a.Type == AlarmType.Above && value > a.Limit) ||
                                                (a.Type == AlarmType.Below && value < a.Limit)));
        return Task.FromResult(invokedAlarms.AsEnumerable());
    }

    public Task<Alarm> Create(Alarm newAlarm)
    {
        _alarms.Add(newAlarm);
        return Task.FromResult(newAlarm);
    }

    public Task<Alarm> Delete(string alarmName)
    {
        var alarm = _alarms.FirstOrDefault(a => a.AlarmName == alarmName);
        if (alarm != null) _alarms.Remove(alarm);
        return Task.FromResult(alarm);
    }

    public Task<Alarm> Update(Alarm updatedAlarm)
    {
        var alarm = _alarms.FirstOrDefault(a => a.AlarmName == updatedAlarm.AlarmName);
        if (alarm == null) return Task.FromResult(updatedAlarm);
        _alarms.Remove(alarm);
        _alarms.Add(updatedAlarm);

        return Task.FromResult(updatedAlarm);
    }

    public Task<IEnumerable<Alarm>> GetByTagId(string tagId)
    {
        var tagAlarms = _alarms.Where(a => a.TagId == tagId);
        return Task.FromResult(tagAlarms.AsEnumerable());
    }

    public Alarm SaveTriggeredAlarm(Alarm triggeredAlarm)
    {
        dbContext.Alarms.Add(triggeredAlarm);
        dbContext.SaveChanges();
        return triggeredAlarm;
    }

    public List<Alarm> GetTriggeredAlarmsDuringInterval(DateTime start, DateTime end)
    {
        return dbContext.Alarms
            .Where(a => start <= a.Time && a.Time <= end)
            .OrderBy(a => a.Priority)
            .ThenByDescending(a => a.Time)
            .ToList();
    }

    public List<Alarm> GetTriggeredAlarmsByPriority(AlarmPriority priority)
    {
        return dbContext.Alarms
            .Where(a => a.Priority == priority)
            .OrderByDescending(a => a.Time)
            .ToList();
    }
}