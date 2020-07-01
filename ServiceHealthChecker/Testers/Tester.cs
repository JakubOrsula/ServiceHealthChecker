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
            
            await Task.Delay(5000);
            return ServiceStatus.Timeout;
        }
    }
}
