using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VårbyBilbiotek.Models;
using Helpers;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace VårbyBilbiotek.Data
{
    public enum BookTitles { [Description("Metro 2033")] Metro, [Description("Lord of the rings")] Lotr, [Description("Judge Dredd")] Dredd,
        [Description("Game Of Thrones")] GOT, [Description("Silent Hill")] SH, [Description("Batman Bin Suparman")] FMARVEL, Halo,
        [Description("The Picture of Dorian Gray")] DG, [Description("Never Let Me Go")] Never, [Description("The Road")] VÄG, [Description("March: Book One (Oversized Edition)")] Stor, [Description("The Hobbit")] Liten,
        [Description("Pride and Prejudice")] TroddeDettaVaEnFilm, [Description("A Tale of Two Cities")] SimCity, [Description("Crime and Punishment")] HörtDennaVaBra, [Description("We Should All Be Feminists")] IVissaFall, [Description("Persepolis")] NuräckerD,
    }

    internal class DataAccess
    {
        internal csSeedGenerator rnd = new csSeedGenerator();


        public void CreateFiller()
        {
            using (var context = new Context())
            {

                for (int i = 0; i < 10; i++)
                {
                    Person person= new Person();

                    person.FirstName = rnd.FirstName;
                    person.LastName = rnd.LastName;
                    
                    Book book = new Book();
                    book.Year = rnd.Next(1900, 2023);
                    book.Title = GetEnumDescription(rnd.FromEnum<BookTitles>());

                    LoanCard loanCard= new LoanCard();

                    Autor autor= new Autor();

                    autor.Name = rnd.FullName;

                    context.Persons.Add(person);
                    context.Books.Add(book);
                    context.Autors.Add(autor);
                    context.LoanC.Add(loanCard);

                    
                }

                context.SaveChanges();
            }
        }


        public void MarkBookAsNotLoaned(int bookId)
        {
            using (var context = new Context())
            {
                var book = context.Books.Include(b => b.LoanCard).FirstOrDefault(b => b.Id == bookId);

                if (book != null)
                {
                    
                    book.LoanCardId = null;

                  
                    if (book.LoanCard != null)
                    {
                        book.LoanCard.Books.Remove(book);
                        book.LoanDate = null;
                        book.ReturnDate= null;
                        book.Loaned = false;
                    }

                    // Save changes to the database
                    context.SaveChanges();
                }
            }
        }


        public void AddPersonToDatabase(string firstName, string lastName)
        {
            using (var context = new Context())
            {               
                var person = new Person
                {
                    FirstName = firstName,
                    LastName = lastName
                };
               
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }

        public void AddBookToDatabase(string title, params int[] autorIds)
        {
            using (var context = new Context())
            {
                var autors = context.Autors.Where(a => autorIds.Contains(a.Id)).ToList();

                var book = new Book
                {
                    Title = title,
                    Autors = autors,
                    Year = new Random().Next(1900, 2023)
                    
                };

                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public void AddAutorToDatabase(string fullName)
        {
            using (var context = new Context())
            {
                var autor = new Autor
                {
                    Name = fullName
                    
                };

                context.Autors.Add(autor);
                context.SaveChanges();
            }
        }



        public void AddLoanCardToPerson(int id)
        {
            using (var context = new Context())
            {
                // Step 1: Retrieve the Person
                var person = context.Persons.Find(id);

                if (person == null)
                {
                    // Handle the case where the person with the specified ID doesn't exist
                    // You can throw an exception, log a message, or take appropriate action
                    return;
                }

                // Step 2: Create a new LoanCard
                var loanCard = new LoanCard();

                // Step 3: Link the LoanCard to the Person
                person.loanCard = loanCard;

                // Step 4: Save changes to the database
                context.SaveChanges();
            }
        }

        public void AddBookIdToPersonLoanCard(int personId, int bookId)
        {
            using (var context = new Context())
            {
               
                var person = context.Persons.Include(p => p.loanCard).SingleOrDefault(p => p.Id == personId);

                if (person == null)
                {
                   
                    return;
                }

                
                if (person.loanCard == null)
                {
                    Console.WriteLine($"Person #{personId} has no LoanCard");
                    return;
                }
                             
                var book = context.Books.Find(bookId);

                if (book != null)
                {
                    
                    book.LoanCardId = person.loanCard.Id;
                    book.Loaned = true;
                    book.LoanDate = DateTime.Now;
                    book.ReturnDate = book.LoanDate?.AddDays(14);
                    context.SaveChanges(); 
                }
                else
                {
                    Console.WriteLine($"No book with id #{bookId}\nReturn nothing!");
                }
            }
        }


        public void Clear()
        {
            using (var context = new Context())
            {
                var allPersons = context.Persons.ToList();
                context.Persons.RemoveRange(allPersons);
                var allBooks = context.Books.ToList();
                context.Books.RemoveRange(allBooks);
                var allAutors = context.Autors.ToList();
                context.Autors.RemoveRange(allAutors);
                var allLoanC = context.LoanC.ToList();
                context.RemoveRange(allLoanC);

                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Persons', RESEED, 0)");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Books', RESEED, 0)");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Autors', RESEED, 0)");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('LoanC', RESEED, 0)");

                context.SaveChanges();
            }
        }

        public void RemoveBookFDB(int bookId)
        {
            using (var context = new Context())
            {               
                var RemoveB = context.Books.SingleOrDefault(b => b.Id == bookId);
                
                if (RemoveB != null)
                {                   
                    context.Books.Remove(RemoveB);   
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No book with Id# {bookId}");
                }
            }
        }

        public void RemovePersonFDB(int PersonId)
        {
            using (var context = new Context())
            {
                var RemoveP = context.Persons.Include(p => p.loanCard).SingleOrDefault(p => p.Id == PersonId);

                if (RemoveP != null)
                {
                    var PersonsLoanCardId = RemoveP.loanCard.Id;
                    context.Persons.Remove(RemoveP);

                    var bookLinqP = context.Books.SingleOrDefault(b => b.LoanCardId == PersonsLoanCardId);

                    if (bookLinqP != null)
                    {
                        bookLinqP.Loaned = false;
                        bookLinqP.LoanCard = null;
                        bookLinqP.LoanDate = null;
                        bookLinqP.ReturnDate = null;
                    }
                    else
                    {
                        Console.WriteLine($"No Book with Id# {PersonId}");
                    }

                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No Person with Id# {PersonId}");
                }
            }
        }

        public void RemoveAuthorFDB(int AuthorId)
        {
            using (var context = new Context())
            {
                var RemoveA = context.Autors.SingleOrDefault(b => b.Id == AuthorId);
                
                if (RemoveA != null)
                {
                    context.Autors.Remove(RemoveA);
                }
                else
                {
                    Console.WriteLine($"No author with id# {AuthorId} ");
                }

                context.SaveChanges();

            }
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return value.ToString();
        }

    }
}
