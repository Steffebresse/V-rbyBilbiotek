using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VårbyBilbiotek.Models
{



    internal class Book
    {

        public int Id { get; set; }
        [MaxLength(50)]
        public string? Title { get; set; }
        public int? Year { get; set; }



        public bool Loaned { get; set; } = false;


        public DateTime? LoanDate { get; set; }
        /*
        public DateTime? LoanDate
        {
            get => _loanDate;
            set
            {
                _loanDate = value;

                if (LoanCardId.HasValue)
                {
                    //funka förihellveteeeeee
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(14);
                    Loaned = true;
                }
                else
                {
                    
                    _loanDate = null;
                    ReturnDate = null;
                    Loaned = false;
                }
            }
        }
        */
        public DateTime? ReturnDate { get; set; } // ska vara returnde date
        public Guid Isbn { get; set; } = Guid.NewGuid();

        public int Grade { get; set; } = new Random().Next(1, 5);

        
        
        public int? LoanCardId { get; set; }
        
        public LoanCard? LoanCard { get; set; }

        public ICollection<Autor>? Autors { get; set; }

        
    }   
}
