using ServiceHealthChecker.DB.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ServiceHealthChecker.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToString3(this ServiceStatus status)
        {
            return "";
        }

        //public static bool Success(this ProbingLog log)
        //{
        //    if (log.Status == ServiceStatus.)
        //}

        public static string CustomToString(this TimeSpan tspn)
        {
            return $"{Math.Floor(tspn.TotalSeconds)}.{tspn.Milliseconds}s";
        }

        public static string ToHttpMethodString(this HttpMethods method)
        {
            switch(method)
            {
                case HttpMethods.GET:
                    return "GET";
                default:
                    return "UNKNOWN";
            }
        }

        public static HttpMethod ToHttpMethodObj(this HttpMethods method)
        {
            //todo dont construct new class use static props
            return new HttpMethod(method.ToHttpMethodString());
        }
    }
}
