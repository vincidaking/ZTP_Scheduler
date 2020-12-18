using FluentEmail.Core;
using SchedulerEmailSender.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;



namespace SchedulerEmailSender.Interface
{
    public interface IEmailServices
    {
        void Send(Person person);
        //Task<string> HtmlString(Person person);
        //Task Send(Person person);

    }
}
