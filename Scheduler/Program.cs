using System;
using System.Timers;
using CsvHelper;
using Serilog;
using System.Globalization;
using System.IO;
using Bogus;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Diagnostics;
using SchedulerEmailSender.Interface;
using SchedulerEmailSender.Services;
using System.Configuration;
using SchedulerLogger.Interface;
using SchedulerLogger.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using SchedulerHangfire.Interface;
using SchedulerHangfire.Services;

namespace Scheduler
{

    class Program
    {

        static void Main(string[] args)
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


            var collection = new ServiceCollection()          
            .AddScoped<ILogServices, LogServices>()
            .AddScoped<IHangFire, HangFire>();


            IServiceProvider serviceProvider = collection.BuildServiceProvider();

            
            var logger = serviceProvider.GetService<ILogServices>();
            var hangfire = serviceProvider.GetService<IHangFire>();



            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Error()
                 .WriteTo.Console()
                 .WriteTo.File(ConfigurationManager.AppSettings["Log"])
                 
                 .CreateLogger();


            //logger.LogTest();


           

            hangfire.StartServer();

            


            Console.WriteLine("Aplikacja została włączona o {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();

           

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
