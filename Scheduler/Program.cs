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


            var collection = new ServiceCollection()
            .AddScoped<IEmialOnTime, EmailOnTime>();
            
        
            IServiceProvider serviceProvider = collection.BuildServiceProvider();
            
            var emailOnTime = serviceProvider.GetService<IEmialOnTime>();

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
