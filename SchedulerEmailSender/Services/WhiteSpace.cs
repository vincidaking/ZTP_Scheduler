﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulerEmailSender.Services
{
    public static class WhiteSpace
    {
       
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
