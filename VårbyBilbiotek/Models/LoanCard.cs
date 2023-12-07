using Microsoft.EntityFrameworkCore;
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

        public int Pin { get; set; } = new Random().Next(1000, 9999);
        
        
        public ICollection<Book>? Books { get; set; }


    }
}
