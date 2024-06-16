using SCADA_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Utilities
{
    public class ValidationService
    {
        public static void ValidateEmptyString(string parameterName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{parameterName} cannot be null or empty.");
            }
        }

        public static void ValidateAlarm(AlarmDto alarm)
        {
            if (alarm == null)
            {
                throw new ArgumentNullException(nameof(alarm), "Alarm cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(alarm.AlarmName))
            {
                throw new ArgumentException("AlarmName cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(alarm.TagId))
            {
                throw new ArgumentException("TagName cannot be null or empty.");
            }
            if (alarm.Limit <= 0)
            {
                throw new ArgumentException("Limit must be greater than zero.");
            }
            if (alarm.Type != "ABOVE" && alarm.Type != "BELOW")
            {
                throw new ArgumentException("Type must be either 'ABOVE' or 'BELOW'.");
            }
        }
    }
}
