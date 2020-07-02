using ServiceHealthChecker.DB.Models;
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
                ServiceID = service.ID,
                ServiceName = service.Name,
                UsedUri = service.URI,
                UsedMethod = service.Method,
                ExpectedResponseCode = service.ExpectedCode,
                UsedTimeout = service.Timeout,
                Status = ServiceStatus.Untested
            };
            HttpResponseMessage response = null;
            try
            {
                switch (service.Method)
                {
                    //todo implement other methods
                    case HttpMethods.GET:
                        response = await httpClient.GetAsync(service.URI);
                        break;
                }
            }
            catch (HttpRequestException e)
            {
                res.Status = ServiceStatus.NetworkError;
                res.ReceivedResponseCode = response.StatusCode;
                return res;
            }
            catch (TaskCanceledException e)
            {
                res.Status = ServiceStatus.Timeout;
                res.ReceivedResponseCode = response.StatusCode;
                return res;
            }

            if (response == null)
            {
                res.Status = ServiceStatus.NetworkError;
                res.ReceivedResponseCode = response.StatusCode;
                return res;
            }

            if (response.StatusCode != service.ExpectedCode)
            {
                res.Status = ServiceStatus.ValidationError;
                res.ReceivedResponseCode = response.StatusCode;
                return res;
            }

            res.Status = ServiceStatus.AliveAndWell;
            res.ReceivedResponseCode = response.StatusCode;
            return res;
        }
    }
}
