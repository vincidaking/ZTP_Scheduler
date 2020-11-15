using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using Razor.Templating.Core;
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
        public async Task<string> HtmlString(Person person)
        {
            return await RazorTemplateEngine.RenderAsync("/Views/View.cshtml", person);

            //return html;
        }

        public async Task Send(Person person)

        
        {


           


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("From Company", ConfigurationManager.AppSettings["MailAddress"]));
            message.To.Add(new MailboxAddress(person.FirstName, WhiteSpace.RemoveWhitespace(person.Email)));
            message.Subject = ConfigurationManager.AppSettings["MessageSubject"];


            var builder = new BodyBuilder();

            person.Discount = person.Discount * 10.0;
                  
            builder.HtmlBody = await HtmlString(person);

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
