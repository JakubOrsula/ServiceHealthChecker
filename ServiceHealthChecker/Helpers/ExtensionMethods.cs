using ServiceHealthChecker.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

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

        public static string GetFullUri(this Service s)
        {
            if (s is null)
            {
                return "";
            }
            if (!s.QueryParams.Any())
            {
                return s.URI.ToString();
            }
            //todo maybe keep it stored encoded
            var parameters = s.QueryParams.Select(param => HttpUtility.UrlEncode(param.Key) + "=" + HttpUtility.UrlEncode(param.Value)).ToArray();
            var queryString = String.Join("&", parameters);
            return s.URI + "?" + queryString;

        }
    }
}
