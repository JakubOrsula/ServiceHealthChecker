using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceHealthChecker.DB.Models
{
    public interface IMyDictionary
    {
        int ID { get; set; }
        int ServiceId { get; set; }
        string Key { get; set; }
        string Value { get; set; }
        Service Service { get; set; }
    }
}
