using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Implementation from 
        //https://marcominerva.wordpress.com/2019/11/07/update-on-using-hostbuilder-dependency-injection-and-service-provider-with-net-core-3-0-wpf-applications/

        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appSettings.json", false, true);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .Build();
        }

        private void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
