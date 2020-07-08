using ServiceHealthChecker.DB.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using Xamarin.Forms.Internals;
using System.Threading;
using ServiceHealthChecker.Helpers;

namespace ServiceHealthChecker.Testers
{
    public static class Tester
    {
        private static HttpClient httpClient = new HttpClient();

        private static void HandleRequestFinish(ref ProbingLog log, ref HttpResponseMessage response)
        {
            log.RequestFinish = DateTime.Now;
            log.ReceivedResponseCode = response.StatusCode;
        }
        

        public static async Task<ProbingLog> TestService(Service service)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var log = new ProbingLog
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
            log.RequestStart = DateTime.Now;
            Task.Run(async () => { await Task.Delay(service.Timeout * 1000); cts.Cancel(); });


            var request = new HttpRequestMessage(service.Method.ToHttpMethodObj(), service.URI);
            foreach(var item in service.Headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            try
            {
                response = await httpClient.SendAsync(request, cts.Token);
            }
            catch (HttpRequestException e)
            {
                HandleRequestFinish(ref log, ref response);
                log.Status = ServiceStatus.NetworkError;
                return log;
            }
            catch (TaskCanceledException e)
            {
                log.Status = ServiceStatus.Timeout;
                return log;
            }
            catch(Exception e) //theres a bug in mono wich causes timout to be thrown as antive exception and I cannot catch it otherwise
            {
                log.Status = ServiceStatus.Timeout;
                return log;
            }

            if (response == null) //should never happen and will throw nullexcpetion
            {
                log.Status = ServiceStatus.NetworkError;
                return log;
            }

            if (response.StatusCode != service.ExpectedCode)
            {
                HandleRequestFinish(ref log, ref response);
                log.Status = ServiceStatus.ValidationError;
                return log;
            }

            HandleRequestFinish(ref log, ref response);
            log.Status = ServiceStatus.AliveAndWell;
            return log;
        }
    }
}
