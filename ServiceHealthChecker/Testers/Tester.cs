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
        public static async Task<ServiceStatus> TestService(Service service)
        {
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
                return ServiceStatus.NetworkError;
            }
            catch (TaskCanceledException e)
            {
                return ServiceStatus.Timeout;
            }

            if (response == null)
            {
                return service.Status;
            }

            if (response.StatusCode != service.ExpectedCode)
            {
                return ServiceStatus.ValidationError;
            }
            
            return ServiceStatus.AliveAndWell;
        }
    }
}
