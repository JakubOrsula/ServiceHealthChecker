﻿using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ServiceHealthChecker.DB.Models
{
    //todo think of a better name
    abstract public class ServiceCollection
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(Service))]
        public int ServiceId { get; set; }
        [ManyToOne]
        public Service Service { get; set; }
    }

    abstract public class KeyValueCollection: ServiceCollection
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class StringCollection:  ServiceCollection
    {
        public string Value { get; set; }
    }

    //unrelated todo inspect malformed header parameters
    public class Header : KeyValueCollection
    {
    }

    public class QueryParam : KeyValueCollection
    {
    }


}