using FluentEmail.Core;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MimeKit;

using SchedulerEmailSender.Interface;
using SchedulerEmailSender.Model;

using Serilog;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace SchedulerEmailSender.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly ILogger<EmailServices> _logger;
        private readonly IServiceProvider _serviceProvider;
        //private readonly IFluentEmail _iFluentEmail;
        public EmailServices(  ILogger<EmailServices> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        public void Send(Person person)       
        {

            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("From Company", ConfigurationManager.AppSettings["MailAddress"]));
            //message.To.Add(new MailboxAddress(person.FirstName, WhiteSpace.RemoveWhitespace(person.Email)));
            //message.Subject = ConfigurationManager.AppSettings["MessageSubject"];

            //var builder = new BodyBuilder();

            //person.Discount = person.Discount * 10.0;

            ////builder.HtmlBody = await HtmlString(person);
            //builder.HtmlBody = "xd";


            //message.Body = builder.ToMessageBody();

            //using (var client = new SmtpClient())
            //{
            //    client.Connect(ConfigurationManager.AppSettings["SmtpHost"],
            //        Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]));


            //    // Note: since we don't have an OAuth2 token, disable
            //    // the XOAUTH2 authentication mechanism.
            //    client.AuthenticationMechanisms.Remove("XOAUTH2");

            //    // Note: only needed if the SMTP server requires authentication
            //    client.Authenticate(ConfigurationManager.AppSettings["MailAddress"],
            //        ConfigurationManager.AppSettings["MailPassword"]);

            //    Log.Error("{@Person}", person);

            //    client.Send(message);
            //    //client.Disconnect(true);
            //};


            /////////////////////////////////////////
            // ten kod wyzej do wysylania emial dziala 
            // z tym ze nie ma tam teamplate 
            //////////////////////////////////
            // _logger.LogInformation(person.ToString());

            //Email.DefaultRenderer = new RazorRenderer();




            // tu to powinno dzialac nie dziala
            // tu masz kod grzeska ja sie na nim wzorowalem 
            // https://github.com/Ryzoo/Scheduler
            Log.Information("{@Person}", person);

            var template = "hi @Model.Name this is a razor template @(5 + 5)!";

            using var scope = _serviceProvider.CreateScope();

            scope.ServiceProvider.GetRequiredService<IFluentEmail>()
                .To(WhiteSpace.RemoveWhitespace(person.Email), person.FirstName)
                .Subject(ConfigurationManager.AppSettings["MessageSubject"])
                //.UsingTemplate(template, person);
            .Send();




            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var response =  scope.ServiceProvider.GetRequiredService<IFluentEmail>()
            //        .To("test@example.com")
            //        .Subject(DateTime.Now.ToLongTimeString())
            //        .Body("test")
            //        .Send();

            //    Log.Error("sdas");

            //    Log.Error("{@Person}", person);

            //}



            //var collection = new ServiceCollection();


            //collection
            //         .AddFluentEmail(ConfigurationManager.AppSettings[$"MailAddress"])
            //        .AddRazorRenderer()
            //        .AddSmtpSender(ConfigurationManager.AppSettings[$"SmtpHost"],
            //         Int32.Parse(ConfigurationManager.AppSettings[$"SmtpPort"]),
            //         ConfigurationManager.AppSettings[$"MailAddress"],
            //         ConfigurationManager.AppSettings[$"MailPassword"]);

          


            //await _iFluentEmail
            //    .To(WhiteSpace.RemoveWhitespace(person.Email),person.FirstName)
            //    .Subject(ConfigurationManager.AppSettings["MessageSubject"])
            //    .UsingTemplate(template,person)
            //    .SendAsync();





        }




    }


    

}
