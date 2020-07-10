using System;
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
        ResponseCodeMismatch,
        BodyValidationFail,
        Timeout,
        Untested
    }

    public enum HttpMethods
    {
        Get,
        Post,
        Put,
        Delete
    }

    public class Service
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public Uri URI { get; set; }
        
        public HttpMethods Method { get; set; }
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BodyMustContain> BodyMustContain { get; set; } = new List<BodyMustContain>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BodyMustNotContain> BodyMustNotContain { get; set; } = new List<BodyMustNotContain>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Header> Headers { get; set; } = new List<Header>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<QueryParam> QueryParams { get; set; } = new List<QueryParam>();
        
        public HttpStatusCode ExpectedResponseCode { get; set; } = HttpStatusCode.OK;
        
        public TimeSpan Timeout { get; set; } = Constants.DefaultTimeout;

        public Service()
        {
            Headers = new List<Header>
            {
                new Header
                {
                    Key = "User-Agent",
                    Value = "ServiceHealthChecker"
                }
            };
        }

    }
}