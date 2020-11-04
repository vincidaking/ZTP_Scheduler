using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using SchedulerEmailSender.Interface;
using SchedulerEmailSender.Services;
using SchedulerHangfire.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerHangfire.Services
{
    public class HangFire : IHangFire
    {

        public void StartServer()
        {

            var collection = new ServiceCollection()
           .AddScoped<IEmailServices, EmailServices>();

            IServiceProvider serviceProvider = collection.BuildServiceProvider();

            var emailOnTime = serviceProvider.GetService<IEmailServices>();

            GlobalConfiguration.Configuration
                //.UseSqlServerStorage(@"Server=.\SQLEXPRESS; Database=Hangfire.Sample; Integrated Security=True");

                .UseMemoryStorage();
            int countPerson = new CSV().CountCSV();

            using (var server = new BackgroundJobServer())
            {
                int time = 0;
                int partia = -1;

                int index = 0;
                int end = 50;

                var tempListPerson = new CSV().ListCSV(index, end);
                for (int i = 0; i < countPerson; i++)
                {
                    if (i % 50 == 0)
                    {
                        time = time + 60;
                        partia++;
                    }

                    //ten timer cos oszukuje i nie dziala tak jak bym oczekiwal 

                    BackgroundJob.Schedule(() => emailOnTime.Send(tempListPerson[i - (partia * 50)]),
                        TimeSpan.FromSeconds(time));

                    index++;
                    end++;

                    if (index % 50 == 0) tempListPerson = new CSV().ListCSV(index, end);
                }

                //Console.WriteLine("Hangfire Server started. Press any key to exit...");
                //Console.ReadKey();
            }
        }
    }
}
