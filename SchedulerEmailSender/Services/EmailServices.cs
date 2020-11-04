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

        public void Send(Person person)
        {
            var fromAddress = new MailAddress("testcsparp@gmail.com", "From Company");
            const string fromPassword = "Qwerty1111!";
            const string subject = "Promocja w Company!";

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

            Log.Error("{@Person}", person);
        }
    }
}
