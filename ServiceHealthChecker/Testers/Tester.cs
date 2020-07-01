using ServiceHealthChecker.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthChecker.Testers
{
    public static class Tester
    {
        public static async Task<ServiceStatus> TestService(Service service)
        {
            await Task.Delay(5000);
            return ServiceStatus.Timeout;
        }
    }
}
