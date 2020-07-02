using System;
using Microsoft.MobileBlazorBindings;
using Microsoft.Extensions.Hosting;
using ServiceHealthChecker.Components;
using ServiceHealthChecker.DB;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ServiceHealthChecker
{
    public class App : Application
    {
        private static DB.ServicesDatabase servicesDb;
        private static DB.LogsDatabase logsDb;
        public static DB.ServicesDatabase ServicesDb => servicesDb ?? (servicesDb = new DB.ServicesDatabase());
        public static DB.LogsDatabase LogsDb => logsDb ?? (logsDb = new DB.LogsDatabase());

        public App()
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                })
                .Build();

            MainPage = new ContentPage();
            // host.AddComponent<HelloWorld>(parent: MainPage);
            host.AddComponent<Homepage>(parent: MainPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
