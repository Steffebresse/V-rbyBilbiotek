using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using VårbyBilbiotek.Data;
using VårbyBilbiotek.Models;

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
                        data.AddBookIdToPersonLoanCard(5, 8);
                        break;
                    case 5:
                        data.AddBookToDatabase("CV bok", 6, 7);
                        break;
                    case 6:
                        data.MarkBookAsNotLoaned(14);
                        break;
                    case 7:
                        data.RemoveBookFDB(7);
                        break;
                    case 8:
                        data.RemovePersonFDB(5);
                        break;
                    case 9:
                        data.RemoveAuthorFDB(7);
                        break;
                    
                        

                }



            } while (choice != -1);
            
            

            

            


            /*

            data.CreateFiller();
            data.Clear();
            data.AddLoanCardToPerson(5);
            data.AddBookIdToPersonLoanCard(5, 8);
            data.AddBookToDatabase("CV bok", 6, 7);
            data.MarkBookAsNotLoaned(14);
            data.RemoveBookFDB(7);
            data.RemovePersonFDB(5);
            data.RemoveAuthorFDB(7);
            */

            









        }

        public static int Menu()
        {
            Console.WriteLine("VårbyBilbioteket\n\n");

            Console.WriteLine("1. Create PlaceHolder items");
            Console.WriteLine("2. Clear the whole database");
            Console.WriteLine("3. Add Loancard to a person");
            Console.WriteLine("4. Add a book to the library");
            Console.WriteLine("5. Unloan a book");
            Console.WriteLine("6. Remove a book from Library");
            Console.WriteLine("7. Remove a customer from Library");
            Console.WriteLine("8. Remove an author from book");
            Console.WriteLine("Q - Quit program");

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
                if (int.TryParse(input,out choice) && choice >= 1 && choice <= 8)
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

        public static void WriteOutPersons(Context context)
        {
            int counter = 0;
            

            foreach (var person in context.Persons)
            {

                

                string personInfo = $"(ID: {person.Id}) : {person.FirstName} {person.LastName} (Has LoanCard: {person.loanCard != null})\n ";
                Console.WriteLine(personInfo);

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
                

                int intChoice = 0;
                string choice;
                int Max = context.Persons.Count();

                Console.WriteLine($"Choose a number between number {intChoice} and {Max}");

                if (int.TryParse(choice = Console.ReadLine(), out intChoice) && intChoice >= 0 && intChoice <= Max)
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