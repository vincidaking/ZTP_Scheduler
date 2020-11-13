using CsvHelper;
using SchedulerEmailSender.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SchedulerHangfire.Services
{
    public class CSV
    {
        public String csvPath { get; set; }

        public CSV()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            String dir = Path.GetDirectoryName(assembly.Location);
            String file = ConfigurationManager.AppSettings[$"MalingCSVPath"];
            this.csvPath = $"{dir}/{file}";
        }

        public List<Person> ListCSV(int startIndex, int endIndex)
        {
            using var reader = new StreamReader(csvPath);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<Person>().Skip(startIndex).Take(endIndex).ToList();
        }

        public int CountCSV()
        {
            using var reader = new StreamReader(csvPath);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<Person>().Count();
        }
    }
}