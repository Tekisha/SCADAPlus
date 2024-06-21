using System.ServiceModel;
using ServiceReference1;

namespace AlarmDisplay;

internal class AlarmClient : IAlarmControllerCallback
{
    private readonly AlarmControllerClientBase _alarmControllerClient;

    public AlarmClient()
    {
        var ic = new InstanceContext(this);
        _alarmControllerClient = new AlarmControllerClientBase(ic, new WSDualHttpBinding(),
            new EndpointAddress("http://localhost:8733/SCADA/AlarmController"));
        _alarmControllerClient.Subscribe();
    }

    public void OnAlarmTriggered(TriggeredAlarmDto triggeredAlarmDto)
    {
        var numberOfTimesToDisplay = 4 - (int)triggeredAlarmDto.Alarm.Priority;

        var alarm = triggeredAlarmDto.Alarm;

        for (var i = 0; i < numberOfTimesToDisplay; i++)
            Console.WriteLine(
                $"{alarm.Time}: {alarm.AlarmName} on {triggeredAlarmDto.TagDescription} (Priority: {alarm.Priority}, Type: {alarm.Type}, Limit: {alarm.Limit})");
        Console.WriteLine();
    }

    public void Close()
    {
        _alarmControllerClient.Close();
    }
}