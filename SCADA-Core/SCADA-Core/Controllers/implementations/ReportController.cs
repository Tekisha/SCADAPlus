using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Enums;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCADA_Core.Controllers.implementations
{
    public class ReportController(IUserService userService, IReportService reportService, ITagService tagService) : IReportController
    {
        public List<TriggeredAlarmDto> GetAlarmsByPriority(AlarmPriority priority, string token)
        {
            if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
            return reportService.GetAlarmsByPriority(priority)
                .Select(ToDto)
                .ToList();
        }

        public List<TriggeredAlarmDto> GetAlarmsDuringInterval(DateTime start, DateTime end, string token)
        {
            if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
            return reportService.GetAlarmsDuringInterval(start, end)
                .Select(ToDto)
                .ToList();
        }

        public List<TagValueDTO> GetAllTagValues(string id, string token)
        {
            if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
            return reportService.GetAllTagValues(id).Select(ToDto).ToList();
        }

        public List<TagValueDTO> GetLatestAnalogInputTagValues(string token)
        {
            if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
            return reportService.GetLatestAnalogInputTagValues().Select(ToDto).ToList();
        }

        public List<TagValueDTO> GetLatetstDigitalInputTagValues(string token)
        {
            if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
            return reportService.GetLatetstDigitalInputTagValues().Select(ToDto).ToList();
        }

        public List<TagValueDTO> GetTagValuesDuringInterval(DateTime start, DateTime end, string token)
        {
            if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
            return reportService.GetTagValuesDuringInterval(start, end).Select(ToDto).ToList();
        }

        private bool ValidateToken(string token)
        {
            return userService.ValidateToken(token);
        }

        private TagValueDTO ToDto(TagValue tagValue)
        {
            return new TagValueDTO
            {
                TagId = tagValue.TagId,
                Name = tagService.GetTag(tagValue.TagId).Description,
                Time = tagValue.Time,
                Value = tagValue.Value
            };
        }

        private TriggeredAlarmDto ToDto(Alarm alarm)
        {
            return new TriggeredAlarmDto
            {
                Alarm = alarm,
                TagDescription = tagService.GetTag(alarm.TagId).Description
            };
        }
    }
}
