﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VårbyBilbiotek.Models
{
    internal class LoanCard
    {
        public int Id { get; set; }

        public string Pin { get; set; } = new Random().Next(1000, 9999).ToString();
        
        
        public ICollection<Book>? Books { get; set; }


    }

    
}
