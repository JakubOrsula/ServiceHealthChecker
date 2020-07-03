using ServiceHealthChecker.DB.Models;
using System;
using System.Collections.Generic;
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
    }
}
