using System;
using System.Net;
using SQLite;

namespace ServiceHealthChecker.DB.Models

{
    public class ProbingLog
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }

        public int ServiceId { get; set; }
        public Uri UsedUri { get; set; }
        public HttpMethods UsedMethod { get; set; }
        public HttpStatusCode UsedExpectedCode { get; set; } = HttpStatusCode.OK;
        public ServiceStatus Status { get; set; } = ServiceStatus.Untested;
        public int UsedTimeout { get; set; } = Constants.DefaultTimeout;
    }
}