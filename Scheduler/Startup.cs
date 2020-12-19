using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchedulerEmailSender.Interface;
using SchedulerEmailSender.Services;
using SchedulerHangfire.Interface;
using SchedulerHangfire.Services;
using Serilog;
using System;
using System.Configuration;


namespace Scheduler
{
    public class Startup
    {
        // to zostalo dorobione i tu niby jest cale DI i konfiguracja fluentemail
        IConfigurationRoot Configuration { get; }

        public Startup()
        {

      
            var builder = new ConfigurationBuilder()
                ;
                //.AddJsonFile("appsettings.json");
                

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddLogging();
            
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddSingleton<IHangFire, HangFire>();
            services.AddSingleton<IEmailServices, EmailServices>();

            services.AddFluentEmail(ConfigurationManager.AppSettings[$"MailAddress"])
                    .AddSmtpSender(ConfigurationManager.AppSettings[$"SmtpHost"],
                     Int32.Parse(ConfigurationManager.AppSettings[$"SmtpPort"]),
                     ConfigurationManager.AppSettings[$"MailAddress"],
                     ConfigurationManager.AppSettings[$"MailPassword"])
                    //.AddRazorRenderer()
                    ;

        }
    }
}
