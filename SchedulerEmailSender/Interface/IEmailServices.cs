using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace SchedulerEmailSender.Interface
{
    public interface IEmailServices
    {
        void OnTimedEvent(Object source, ElapsedEventArgs e);
        void Start();
        void StopProgram();
    }
}
