using System;
using SQLite;

namespace ServiceHealthChecker.DB.Models
{
    public class Service
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; } //todo whats the id of not yet inserted row?

        public string Name { get; set; }
        public Uri URI { get; set; }
        public string Method { get; set; }
        public int Timeout { get; set; } = Constants.DefaultTimeout; //timeout in seconds
    }
}