using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceHealthChecker.DB.Models;

namespace ServiceHealthChecker.Helpers
{
    public static class Convertors
    {
        public static IEnumerable<QueryParam> QueryParamsExtractor(string url)
        {
            var querySeparatorIndex = url.IndexOf('?');
            if (querySeparatorIndex != -1 && url.Length != querySeparatorIndex + 1)
            {
                var parameters = HttpUtility.ParseQueryString(url.Substring(querySeparatorIndex+1));
                return parameters.AllKeys.SelectMany(parameters.GetValues, (k, v) => new QueryParam{ Key = k, Value = v });
            }
            return new QueryParam[0];
        }
    }
}