using SchedulerEmailSender.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace SchedulerEmailSender.Interface
{
    public interface IEmailServices
    {
        void Send(Person person);
    }
}
