﻿using System;
using System.Net;
using System.Runtime.CompilerServices;
using SQLite;

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
        public int ID { get; set; } //todo whats the id of not yet inserted row?
        public string Name { get; set; }
        public Uri URI { get; set; }
        public HttpMethods Method { get; set; }
        public HttpStatusCode ExpectedCode { get; set; } = HttpStatusCode.OK;
        public ServiceStatus Status { get; set; } = ServiceStatus.Untested;
        public int Timeout { get; set; } = Constants.DefaultTimeout; //timeout in seconds

    }
}