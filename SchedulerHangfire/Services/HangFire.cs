using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchedulerEmailSender.Interface;
using SchedulerEmailSender.Services;
using SchedulerHangfire.Interface;
using Serilog;
using System;


namespace SchedulerHangfire.Services
{
    public class HangFire : IHangFire
    {

        private readonly IEmailServices _emailServices;
        private readonly ILogger<HangFire> _logger;

        public HangFire( ILogger<HangFire> logger, IEmailServices emailServices)
        {
            _emailServices = emailServices;
            _logger = logger;
        }

        public void StartServer()
        {

           // var collection = new ServiceCollection()
           //.AddScoped<IEmailServices, EmailServices>();

           // IServiceProvider serviceProvider = collection.BuildServiceProvider();

           // var emailOnTime = serviceProvider.GetService<IEmailServices>();

            GlobalConfiguration.Configuration
                //.UseSqlServerStorage(@"Server=.\SQLEXPRESS; Database=Hangfire.Sample; Integrated Security=True");
                .UseMemoryStorage();


            //_logger.LogInformation("Start hangfile");
            //Log.Error("xddd");

            int countPerson = new CSV().CountCSV();

            using (var server = new BackgroundJobServer())
            {
                int time = 0;
                int partia = -1;

                int index = 0;
                //int end = 50;
                int end = 200;

                //var tempListPerson = new CSV().ListCSV(index, end)
                var tempListPerson = new CSV().ListCSV(index, end);


                //for (int i = 0; i < countPerson; i++)
                //{
                //    if (i % 50 == 0)
                //    {
                //        time = time + 60;
                //        partia++;
                //    }

                //    //ten timer cos oszukuje i nie dziala tak jak bym oczekiwal 

                //    //BackgroundJob.Schedule(() => emailOnTime.Send(tempListPerson[i - (partia * 50)]),
                //    //    TimeSpan.FromSeconds(time));

                //    //_logger.LogInformation("xdx");
                //    Log.Information("{@Person}", tempListPerson[i]);


                //   // _emailServices.Send(tempListPerson[i - (partia * 50)]);

                //    index++;
                //    end++;

                //    if (index % 50 == 0)
                //    {
                //        tempListPerson = new CSV().ListCSV(index, end);

                //    }

                //}

                /////////////////////////////////////
                // ten hangfile do konca nie dziala ale tym bym sie nie przemowal tzn BackgroundJob
                //i tu fajnie leci do emiala, wyzej kod odpowiadal za to zeby ladowac na rady zadania do hangfilera ale jak sie okazalo to chyba tez chujowo bylo zrobione xd

                ////////////////////////////////////


                for (int i = 0; i < countPerson; i++)
                {
                    //Log.Information("{@Person}", tempListPerson[i]);
                    _emailServices.Send(tempListPerson[i]);
                    //BackgroundJob.Schedule(() => _emailServices.Send(tempListPerson[i]),
                    //TimeSpan.Zero);
                }


                //Console.WriteLine("Hangfire Server started. Press any key to exit...");
                //Console.ReadKey();
            }
        }
    }
}
