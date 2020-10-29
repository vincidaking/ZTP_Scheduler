using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Scheduler.Services
{
    public interface IEmialOnTime
    {

        void OnTimedEvent(Object source, ElapsedEventArgs e);
        void Start();

        void StopProgram();

    }
}
