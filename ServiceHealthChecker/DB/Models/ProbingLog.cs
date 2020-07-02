using System;
using System.Net;
using SQLite;

namespace ServiceHealthChecker.DB.Models

{
    public class ProbingLog
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }

        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Uri UsedUri { get; set; }
        public HttpMethods UsedMethod { get; set; }
        public HttpStatusCode ExpectedResponseCode { get; set; }
        public HttpStatusCode ReceivedResponseCode { get; set; }
        public ServiceStatus Status { get; set; } = ServiceStatus.Untested;
        public int UsedTimeout { get; set; } = Constants.DefaultTimeout;
    }
}