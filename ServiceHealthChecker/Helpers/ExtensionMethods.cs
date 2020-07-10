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
                case HttpMethods.Get:
                    return "GET";
                case HttpMethods.Put:
                    return "PUT";
                case HttpMethods.Post:
                    return "POST";
                case HttpMethods.Delete:
                    return "DELETE";
                default: //will never happen
                    return "UNKNOWN";
            }
        }

        public static HttpMethod ToHttpMethodObj(this HttpMethods method)
        {
            switch(method)
            {
                case HttpMethods.Get:
                    return HttpMethod.Get;
                case HttpMethods.Put:
                    return HttpMethod.Put;
                case HttpMethods.Post:
                    return HttpMethod.Post;
                case HttpMethods.Delete:
                    return HttpMethod.Delete;
                default: //will never happen
                    return new HttpMethod("UNKNOWN");
            }
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
            var parameters = s.QueryParams.Select(param => HttpUtility.UrlEncode(param.Key) + "=" + HttpUtility.UrlEncode(param.Value)).ToArray();
            var queryString = String.Join("&", parameters);
            return s.URI + "?" + queryString;

        }
    }
}
