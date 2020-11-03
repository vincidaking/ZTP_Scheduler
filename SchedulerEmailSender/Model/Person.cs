using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerEmailSender.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Discount { get; set; }
        public override string ToString()
        {
            return $"{Id}, {FirstName}, {LastName}, {Email}, {Discount}";
        }
    }
}
