using System;

namespace LMS.Presentation.UI
{
    public class MainMenu
    {
        private readonly BookUI _bookUI;

        public MainMenu(BookUI bookUI)
        {
            _bookUI = bookUI;
        }

        public void Show()
        {
            bool exit = false;

            while (!exit)
            {
                // menu to be designed to include members and transactions
                Console.Clear();
                Console.WriteLine("########## LIBRARY MANAGEMENT SYSTEM ##########");
                Console.WriteLine("1. Add new book");
                Console.WriteLine("2. View books");
                Console.WriteLine("3. Delete book");
                // Members section to be added
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        _bookUI.AddBookFlow();
                        break;                    
                    
                    case "2":
                        _bookUI.ViewBooksFlow();
                        break;                      
                    
                    case "3":
                        _bookUI.DeleteBookFlow();
                        break;

                    case "0":
                        exit = true;
                        Console.WriteLine("\nQuitting the app. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("\nPlease insert a valid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}