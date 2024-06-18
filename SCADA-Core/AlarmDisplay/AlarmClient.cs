using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AlarmDisplay
{
    internal class AlarmClient: ServiceReference1.IAlarmControllerCallback
    {
        private ServiceReference1.AlarmControllerClientBase alarmControllerClient;

        public AlarmClient()
        {
            InstanceContext ic = new InstanceContext(this);
            alarmControllerClient = new AlarmControllerClientBase(ic, new WSDualHttpBinding(), new EndpointAddress("http://localhost:8733/SCADA/AlarmController"));
            alarmControllerClient.Subscribe();
        }

        public void Close()
        {
            alarmControllerClient.Close();
        }

        public void OnAlarmTriggered(TriggeredAlarmDto triggeredAlarmDto)
        {

            int numberOfTimesToDisplay = 1;
            switch(triggeredAlarmDto.Alarm.Priority)
            {
                case AlarmPriority.Low:
                    numberOfTimesToDisplay = 1;
                    break;
                case AlarmPriority.Medium:
                    numberOfTimesToDisplay = 2;
                    break;
                case AlarmPriority.High:
                    numberOfTimesToDisplay = 3;
                    break;
            }

            Alarm alarm = triggeredAlarmDto.Alarm;

            for (int i = 0; i < numberOfTimesToDisplay; i++)
            {
                Console.WriteLine($"{alarm.Time}: {alarm.AlarmName} on {triggeredAlarmDto.TagDescription} (Priority: {alarm.Priority}, Type: {alarm.Type}, Limit: {alarm.Limit})");
            }
            Console.WriteLine();
        }
    }
}
