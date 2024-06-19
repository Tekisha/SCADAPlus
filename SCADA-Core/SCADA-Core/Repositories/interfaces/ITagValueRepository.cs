﻿using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.interfaces
{
    public interface ITagValueRepository
    {
        public void Add(string tagId, double value);
        public IEnumerable<TagValue> GetAll();
        public TagValue GetNewest(string tagId);
        List<TagValue> GetTagValuesDuringInterval(DateTime start, DateTime end);
        List<TagValue> GetLatestAnalogInputTagValues();
        List<TagValue> GetLatetstDigitalInputTagValues();
        List<TagValue> GetAllTagValues(string id);
    }
}
