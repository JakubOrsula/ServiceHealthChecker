using System;
using System.Net;
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
        public int ID { get; set; }
        public string Name { get; set; }
        public Uri URI { get; set; }
        public HttpMethods Method { get; set; }
        //todo rename to expected response
        public HttpStatusCode ExpectedCode { get; set; } = HttpStatusCode.OK;
        
        //todo remove the following prop
        public ServiceStatus Status { get; set; } = ServiceStatus.Untested;
        public int Timeout { get; set; } = Constants.DefaultTimeout; //timeout in seconds

    }
}