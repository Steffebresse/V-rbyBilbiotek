﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VårbyBilbiotek.Models
{
    internal class Log
    {
        public int Id { get; set; }

        public static int Count;


        public string Name { get; set; }

        public string? Title { get; set; }

        public DateTime DayBookWasReturned { get; set; }

        
        public Book? Book { get; set; }

        public Log()
        {
            ++Count;
            Name = "Log:" + " " + Count;
        }

    }
}
