﻿using System;
using System.Collections.Generic;
using System.Net;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ServiceHealthChecker.DB.Models
{
    public enum ServiceStatus
    {
        AliveAndWell,
        NetworkError,
        ValidationError,
        Timeout,
        Untested
    }

    public enum HttpMethods
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class Service
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        public string Name { get; set; }
        public Uri URI { get; set; }
        public HttpMethods Method { get; set; }

        [OneToMany]
        public List<ServiceHeaders> Headers { get; set; } = new List<ServiceHeaders>();
        //todo rename to expected response
        public HttpStatusCode ExpectedCode { get; set; } = HttpStatusCode.OK;
        public int Timeout { get; set; } = Constants.DefaultTimeout; //timeout in seconds

    }
}