using CsvHelper;
using MailKit.Net.Smtp;
using MimeKit;
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

using System.Text;
using System.Timers;

namespace SchedulerEmailSender.Services
{
    public class EmailServices : IEmailServices
    {

        public void Send(Person person)
        {
            //var fromAddress = new MailAddress(ConfigurationManager.AppSettings["MailAddress"], "From Company");
            //string fromPassword = ConfigurationManager.AppSettings["MailPassword"];
            //string subject = ConfigurationManager.AppSettings["MessageSubject"];
            //Console.WriteLine(subject);

            //var toAddress = new MailAddress(person.Email, "To" + person.FirstName);
            //var body = "Witaj " + person.FirstName + " masz u nas " + person.Discount * 100 + "% zniżki!";




            //var smtp = new SmtpClient
            //{
            //    Host = ConfigurationManager.AppSettings["SmtpHost"],
            //    Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]),
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = true,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //};
            //using var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //};
            //smtp.Send(message);

            //Log.Error("{@Person}", person);


           

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("From Company", ConfigurationManager.AppSettings["MailAddress"]));
            message.To.Add(new MailboxAddress(person.FirstName, WhiteSpace.RemoveWhitespace(person.Email)));
            message.Subject = ConfigurationManager.AppSettings["MessageSubject"];


            var builder = new BodyBuilder();

            //builder.HtmlBody = $"<p>Hey {person.FirstName},<br> otrzymujesz od nas {person.Discount}% " +
            //    $"znizki na najblizsze zakupy" +
            //    $"<p> Zapraszamy";

            

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(ConfigurationManager.AppSettings["SmtpHost"],
                    Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]));


                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(ConfigurationManager.AppSettings["MailAddress"], 
                    ConfigurationManager.AppSettings["MailPassword"]);

                client.Send(message);
                //client.Disconnect(true);
            };

            Log.Error("{@Person}", person);
        }

        
    }
}
