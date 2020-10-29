using CsvHelper;
using Scheduler.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Timers;

namespace Scheduler.Services
{

    public class EmailOnTime : IEmailOnTime
    {
        private System.Timers.Timer aTimer;
        private int index = 0;
        private readonly string IMAGE_DIR = "Date";
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var fromAddress = new MailAddress("testcsparp@gmail.com", "From Company");
            const string fromPassword = "Qwerty1111!";
            const string subject = "Promocja w Company!";

            using var reader = new StreamReader(@"C:\Users\Kamil\Downloads\Scheduler\Scheduler\Scheduler\Date\mailing.csv");

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
