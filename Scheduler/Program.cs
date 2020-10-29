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
using Scheduler.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Diagnostics;


namespace Scheduler
{

    class Program
    {

        static void Main(string[] args)
        {

            //todo data context 
            //todo loger w serwisie
            //todo konfiguracja (ścieżka do pliku csv, ustawienia smtp itp)
            //todo ścieżka względna do pliku (relative path)
            //todo generowanie treści: mamy łączenie stringów, ma być np. html template
            //todo zamiast timera do wysyłania co minutę można użyć np. hanfire 
            //            -podzielić na projekty 
            //- Hangfire użyć można do schedulowania / zarządzania taskami(zamiast timera)

            var collection = new ServiceCollection()
            .AddScoped<IEmailOnTime, EmailOnTime>();


            IServiceProvider serviceProvider = collection.BuildServiceProvider();

            var emailOnTime = serviceProvider.GetService<IEmailOnTime>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.Console()
                .WriteTo.File(@"C:\Users\Kamil\Downloads\Scheduler\Scheduler\Scheduler\Log\myapp.txt")
                .CreateLogger();

            emailOnTime.Start();


            Console.WriteLine("Aplikacja została włączona o {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();

            emailOnTime.StopProgram();

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
