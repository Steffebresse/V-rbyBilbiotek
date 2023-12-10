using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VårbyBilbiotek.Models
{
    internal class Autor
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }


        public ICollection<Book>? Books { get; set; }

        public Autor()
        {
            Books = new List<Book>();
        }
    }
}
