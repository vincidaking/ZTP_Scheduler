using System;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchedulerHangfire.Interface;
using Serilog;

namespace Scheduler
{

    class Program
    {
        
        static void Main(string[] args )
        {

            //Zrobione czy dobrze nie wiem 
            //todo konfiguracja (ścieżka do pliku csv, ustawienia smtp itp)
            //todo zamiast timera do wysyłania co minutę można użyć np. hanfire 
            //            -podzielić na projekty 
            //- Hangfire użyć można do schedulowania / zarządzania taskami(zamiast timera)


            //todo data context     tego nie czaje, data context zwykle chyba prze bazie danych byl 

            //todo loger w serwisie
            //todo ścieżka względna do pliku (relative path)
            //todo generowanie treści: mamy łączenie stringów, ma być np. html template


            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .WriteTo.File(ConfigurationManager.AppSettings[$"txt"], rollingInterval: RollingInterval.Day)
              .CreateLogger();

            //CreateHostBuilder(args).Build().Run();

            IServiceCollection services = new ServiceCollection();
            // Startup.cs finally :)
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);

            //var logger = serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .CreateLogger<Program>();

            

            Log.Information("dziala");


            // Get Service and call method

            var service = serviceProvider.GetService<IHangFire>();
            service.StartServer();

            int temp;

            do
            {
                Log.Information("wcisnij 0 [ponowne wyslanie emial]");
                temp = int.Parse(Console.ReadLine());
                service.StartServer();

            } while (temp == 0);


        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //   Host.CreateDefaultBuilder(args)
        //       .UseSerilog();
        //.UseWindowsService()
        //.ConfigureServices((_, services) =>
        //{
        //    services
        //    .AddLogging()
        //    .AddTransient<IHangFire, HangFire>()
        //    .AddTransient<IEmailServices, EmailServices>()
        //    .AddFluentEmail(ConfigurationManager.AppSettings[$"MailAddress"])
        //    .AddRazorRenderer()
        //    .AddSmtpSender(ConfigurationManager.AppSettings[$"SmtpHost"],
        //    int.Parse(ConfigurationManager.AppSettings[$"SmtpPort"]));

        //});








    }
}
