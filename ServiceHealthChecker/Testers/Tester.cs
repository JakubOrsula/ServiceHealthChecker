using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ServiceHealthChecker.DB.Models;
using ServiceHealthChecker.Helpers;

namespace ServiceHealthChecker.Testers
{
    public static class Tester
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private static void HandleRequestFinish(ref ProbingLog log, ref HttpResponseMessage response)
        {
            log.RequestFinish = DateTime.Now;
            log.ReceivedResponseCode = response.StatusCode;
        }


        public static async Task<ProbingLog> TestService(Service service)
        {
            var cts = new CancellationTokenSource();
            var log = new ProbingLog
            {
                ServiceID = service.ID,
                ServiceName = service.Name,
                UsedUri = service.URI,
                UsedMethod = service.Method,
                ExpectedResponseCode = service.ExpectedResponseCode,
                UsedTimeout = service.Timeout,
                Status = ServiceStatus.Untested
            };
            HttpResponseMessage response = null;
            log.RequestStart = DateTime.Now;

            //this is not ideal, but setting timeout in HttpClient causes uncatchable native exception 
            Task.Run(async () =>
            {
                await Task.Delay(service.Timeout);
                cts.Cancel();
            });

            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };

            var request = new HttpRequestMessage(service.Method.ToHttpMethodObj(), service.GetFullUri());
            foreach (var item in service.Headers)
                try
                {
                    request.Headers.Add(item.Key, item.Value);
                }
                catch (Exception e)
                {
                    //don't send invalid headers (parser is for some reason private so we can't check validity at entering
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
            catch (Exception e)
            {
                log.Status = ServiceStatus.Timeout;
                return log;
            }

            if (response == null)
            {
                log.Status = ServiceStatus.NetworkError;
                return log;
            }

            if (response.StatusCode != service.ExpectedResponseCode)
            {
                HandleRequestFinish(ref log, ref response);
                log.Status = ServiceStatus.ResponseCodeMismatch;
                return log;
            }

            HandleRequestFinish(ref log, ref response);

            log.Status = ServiceStatus.AliveAndWell;
            if (service.BodyMustContain.Any() || service.BodyMustNotContain.Any())
            {
                var body = await response.Content.ReadAsStringAsync();
                // idea: use regex
                if (service.BodyMustContain.Any(s => !body.Contains(s.Value)))
                    log.Status = ServiceStatus.BodyValidationFail;

                if (service.BodyMustNotContain.Any(s => body.Contains(s.Value)))
                    log.Status = ServiceStatus.BodyValidationFail;
            }


            return log;
        }
    }
}