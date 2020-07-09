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
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        //todo rename to body must contain
        public List<StringCollection> ResponseMustContain { get; set; } = new List<StringCollection>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Header> Headers { get; set; } = new List<Header>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<QueryParam> QueryParams { get; set; } = new List<QueryParam>();

        //todo rename to expected response
        public HttpStatusCode ExpectedCode { get; set; } = HttpStatusCode.OK;
        public int Timeout { get; set; } = Constants.DefaultTimeout; //todo refactor to timespan

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