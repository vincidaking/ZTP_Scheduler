using CsvHelper;
using SchedulerEmailSender.Interface;
using SchedulerEmailSender.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Timers;

namespace SchedulerEmailSender.Services
{
    public class EmailServices : IEmailServices
    {
        private System.Timers.Timer aTimer;
        private int index = 0;
        private readonly string IMAGE_DIR = "Date";
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var fromAddress = new MailAddress("testcsparp@gmail.com", "From Company");
            const string fromPassword = "Qwerty1111!";
            const string subject = "Promocja w Company!";

            //using var reader = new StreamReader(@"C:\Users\Kamil\Desktop\MGR\semestr 1\ZTP\ZTP_Scheduler\Scheduler\Data\mailing.csv");
            using var reader = new StreamReader(ConfigurationManager.AppSettings["MalingCSV"]);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            int start = index;
            var records = csv
                .GetRecords<Person>()
                .Skip(index)
                .Take(100);

            int stop = 0;
            foreach (var person in records)
            {
                if (stop >= 100) break;

                var toAddress = new MailAddress(person.Email, "To" + person.FirstName);
                var body = "Witaj " + person.FirstName + " masz u nas " + person.Discount * 100 + "% zniżki!";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };
                //smtp.Send(message);
                index++;
                stop++;
                Log.Error("{@Person}", person);
            }
        }

        public void Start()
        {
            aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void StopProgram()
        {
            aTimer.Stop();
        }
    }
}
