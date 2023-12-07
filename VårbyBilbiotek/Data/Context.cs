using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VårbyBilbiotek.Models;

namespace VårbyBilbiotek.Data
{
    internal class Context : DbContext
    {
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<LoanCard> LoanC { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Isbn);

            base.OnModelCreating(modelBuilder);
        }
        Tar bort denna sålänge, detta är för att säga specifikt att isbn är nyckeln
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Loaned)
                .HasColumnName("Loaned")
                .HasColumnType("bit");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=STEFANS-DATOR\\MSSQLSERVER02; Database=NewtonLibraryStefan; Trusted_Connection=True; Trust Server Certificate =Yes; User Id=NewtonLibraryStefan password=NewtonLibraryStefan");
        }

    }
}
