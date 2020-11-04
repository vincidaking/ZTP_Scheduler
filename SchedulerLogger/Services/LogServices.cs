using SchedulerLogger.Interface;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SchedulerLogger.Services
{
    public class LogServices : ILogServices
    {
        public void LogTest()
        {
            var log = new LoggerConfiguration()
                  .MinimumLevel.Error()
                  //.WriteTo.ToString()
                  //.WriteTo.File(ConfigurationManager.AppSettings["Log"])
                  .CreateLogger()
                  ;
            //Console.WriteLine(log);
        }
    }
}
