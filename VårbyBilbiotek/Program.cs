using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using VårbyBilbiotek.Data;
using VårbyBilbiotek.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VårbyBilbiotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            int choice;
            DataAccess data = new DataAccess();
            do
            {
                choice = Menu();
                MenuChoice(choice, context, data);
       
            } while (choice != -1);
            
            

        }



        public static int Menu()
        {
            Console.WriteLine("VårbyBilbioteket\n\n");

            Console.WriteLine("1. Create Placeholder items");
            Console.WriteLine("2. Clear the whole database");
            Console.WriteLine("3. Add Loancard to a person");
            Console.WriteLine("4. Add a book to person");
            Console.WriteLine("5. Add book to library");
            Console.WriteLine("6. Unloan a book");
            Console.WriteLine("7. Remove book from library");
            Console.WriteLine("8. Remove person from library");
            Console.WriteLine("9 - Remove autor from database");
            Console.WriteLine("10 - Write out all information in library");
            Console.WriteLine("Q - quit");

            //data.CreateFiller();
            //data.Clear();
            //data.AddLoanCardToPerson(5);
            //data.AddBookIdToPersonLoanCard(5, 8);
            //data.AddBookToDatabase("CV bok", 6, 7);
            //data.MarkBookAsNotLoaned(14);
            //data.RemoveBookFDB(7);
            //data.RemovePersonFDB(5);
            //data.RemoveAuthorFDB(7);

            do
            {
                Console.WriteLine("\nPlease choose from the following choices");
                string input = Console.ReadLine();
                int choice = 0;
                if (int.TryParse(input,out choice) && choice >= 1 && choice <= 10)
                {
                    return choice;
                }
                else if(input.ToLower() == "q")
                {
                    return choice = -1;
                }
                else
                {
                    Console.WriteLine("Wrong input, you have to choose between 1 and 9");
                }
                


            } while (true);
            
  
        }

        public static void MenuChoice(int choice, Context context, DataAccess data)
        {
            switch (choice)
            {
                case 1:
                    data.CreateFiller();
                    Console.WriteLine("Created filler!");

                    break;
                case 2:
                    data.Clear();
                    Console.WriteLine("Cleared all data in database");
                    break;
                case 3:
                    WriteOutPersons(context);
                    Console.WriteLine();
                    int ChooseLCTP = ChoosePerson(context);
                    data.AddLoanCardToPerson(ChooseLCTP);
                    break;
                case 4:
                    WriteOutPersons(context);
                    Console.WriteLine();
                    WriteOutBooks(context);
                    Console.WriteLine();
                    int ChoosePersonFC = ChoosePerson(context);
                    Console.WriteLine();
                    int ChooseBookFC = ChooseBook(context);
                    data.AddBookIdToPersonLoanCard(ChoosePersonFC, ChooseBookFC);
                    break;
                case 5:
                    Console.WriteLine("Write the name of the book you want to add");
                    string bookTitle;
                    do
                    {
                        bookTitle = Console.ReadLine();
                    } while (bookTitle.Trim() == "" || bookTitle == null);
                    Console.WriteLine("Choose the autor who has written this book");
                    Console.WriteLine();
                    WriteOutAutors(context);
                    Console.WriteLine();
                    int ChooseAutorFC = ChooseAutor(context);
                    data.AddBookToDatabase(bookTitle, ChooseAutorFC); // den kan ha flera autors, men visste inte hur jag skulle ge den möjligheten inom en metod
                    break;
                case 6:
                    Console.WriteLine("Choose which book has been returned");
                    WriteOutBooks(context);
                    int ChooseBookTR = ChooseBook(context);
                    data.MarkBookAsNotLoaned(ChooseBookTR);
                    break;
                case 7:
                    Console.WriteLine("Choose which book to destroy");
                    WriteOutBooks(context);
                    Console.WriteLine();
                    int BookToDestroy = ChooseBook(context);
                    data.RemoveBookFDB(BookToDestroy);
                    break;
                case 8:
                    Console.WriteLine("Choose which person to remove");
                    Console.WriteLine();
                    WriteOutPersons(context);
                    Console.WriteLine();
                    int PersonToRemove = ChoosePerson(context);
                    data.RemovePersonFDB(PersonToRemove);
                    break;
                case 9:
                    Console.WriteLine("Choose which autor to remove");
                    Console.WriteLine();
                    WriteOutAutors(context);
                    Console.WriteLine();
                    int AutorToRemove = ChooseAutor(context);
                    data.RemoveAuthorFDB(7);
                    break;
                case 10:
                    WriteOutAutors(context);
                    Console.WriteLine();
                    WriteOutBooks(context);
                    Console.WriteLine();
                    WriteOutPersons(context);
                    break;


            }
        }

        public static void WriteOutPersons(Context context)
        {
            int counter = 0;
            

            foreach (var person in context.Persons)
            {

                

                string personInfo = $"(ID: {person.Id}) : {person.FirstName} {person.LastName}\n "; // hade en (Has Loancard) {Person.LoanCard != true} för att visa vilka som har lånekort, men funkar inte :(
                Console.WriteLine(personInfo);

                counter++;

                if (counter % 3 == 0)
                {
                    
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        public static void WriteOutBooks(Context context)
        {
            int counter = 0;


            foreach (var book in context.Books)
            {

                string personInfo = $"(ID: {book.Id}) : Book title: {book.Title} Released: {book.Year} (Loaned: {book.Loaned})\n ";
                Console.WriteLine(personInfo);

                counter++;

                if (counter % 3 == 0)
                {

                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        public static void WriteOutAutors(Context context)
        {
            int counter = 0;


            foreach (var autor in context.Autors)
            {

                string autorInfo = $"(ID: {autor.Id}) : Autor name: {autor.Name}\n ";
                Console.WriteLine(autorInfo);

                counter++;

                if (counter % 3 == 0)
                {

                    Console.WriteLine("----------------------------------------");
                }
            }
        }


        public static int ChoosePerson(Context context)
        {

            do
            {
               int intChoice = 1;
                string choice;
                int Max = context.Persons.Count();

                Console.WriteLine($"Choose a person between number {intChoice} and {Max}");

                if (int.TryParse(choice = Console.ReadLine(), out intChoice) && intChoice >= 1 && intChoice <= Max)
                {
                    return intChoice;
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }

            } while (true);

        }

        public static int ChooseBook(Context context)
        {

            do
            {
                int intChoice = 1;
                string choice;
                int Max = context.Books.Count();

                Console.WriteLine($"Choose an book between number {intChoice} and {Max}");

                if (int.TryParse(choice = Console.ReadLine(), out intChoice) && intChoice >= 1 && intChoice <= Max)
                {
                    return intChoice;
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }

            } while (true);

        }

        public static int ChooseAutor(Context context)
        {

            do
            {
                int intChoice = 1;
                string choice;
                int Max = context.Autors.Count();

                Console.WriteLine($"Choose an autor between number {intChoice} and {Max}");

                if (int.TryParse(choice = Console.ReadLine(), out intChoice) && intChoice >= 1 && intChoice <= Max)
                {
                    return intChoice;
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }

            } while (true);

        }



    }
}