namespace LMS.Presentation.UI
{
    public class MainMenu
    {
        private readonly BookUI _bookUI;
        private readonly MemberUI _memberUI;

        public MainMenu(BookUI bookUI, MemberUI memberUI)
        {
            _bookUI = bookUI;
            _memberUI = memberUI;
        }

        public void Show()
        {
            bool exit = false;

            while (!exit)
            {
                // menu to be designed to include members (and transactions?)
                Console.Clear();
                Console.WriteLine("########## LIBRARY MANAGEMENT SYSTEM ##########");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("1. Add new book");
                Console.WriteLine("2. View books");
                Console.WriteLine("3. Delete book");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("4. Add new member");
                Console.WriteLine("5. View members");
                Console.WriteLine("6. Delete member");
                Console.WriteLine("-----------------------------------------------");                
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

                    case "4":
                        _memberUI.AddMemberFlow();
                        break;                    
                    
                    case "5":
                        _memberUI.ViewMembersFlow();
                        break;                      
                    
                    case "6":
                        _memberUI.DeleteMemberFlow();
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