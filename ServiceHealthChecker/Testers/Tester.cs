using ServiceHealthChecker.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ServiceHealthChecker.Testers
{
    public static class Tester
    {
        private static HttpClient httpClient = new HttpClient();
        public static async Task<ProbingLog> TestService(Service service)
        {
            var res = new ProbingLog
            {
                ServiceId = service.ID,
                UsedUri = service.URI,
                UsedMethod = service.Method,
                UsedExpectedCode = service.ExpectedCode,
                UsedTimeout = service.Timeout,
                Status = ServiceStatus.Untested
            };
            HttpResponseMessage response = null;
            try
            {
                switch (service.Method)
                {
                    case HttpMethods.GET:
                        response = await httpClient.GetAsync(service.URI);
                        break;
                }
            }
            catch (HttpRequestException e)
            {
                res.Status = ServiceStatus.NetworkError;
                return res;
            }
            catch (TaskCanceledException e)
            {
                res.Status = ServiceStatus.Timeout;
                return res;
            }

            if (response == null)
            {
                res.Status = ServiceStatus.NetworkError;
                return res;
            }

            if (response.StatusCode != service.ExpectedCode)
            {
                res.Status = ServiceStatus.ValidationError;
                return res;
            }

            res.Status = ServiceStatus.AliveAndWell;
            return res;
        }
    }
}
