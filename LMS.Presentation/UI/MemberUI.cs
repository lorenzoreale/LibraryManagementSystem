using System.Data.Common;
using System.Runtime.InteropServices;
using LMS.Domain.Entities;
using LMS.Domain.Interfaces;

namespace LMS.Presentation.UI
{
    public class MemberUI
    {
        private readonly IMemberRepository _memberRepo;

        public MemberUI(IMemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }

        public void AddMemberFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Add a new member =====");

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Surname: ");
            string surname = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Date of Birth (DD/MM/YYYY): ");
            string dobInput = Console.ReadLine() ?? "";
            if (!DateOnly.TryParseExact(dobInput, "dd/MM/yyyy", out DateOnly dateOfBirth))
            {
                Console.WriteLine("\nInvalid date format.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;            
            }

            try
            {
                Member newMember = new Member(name, surname, email, dateOfBirth);
                _memberRepo.Add(newMember);
                Console.WriteLine($"\nOperation successful. The member {newMember.Name} has been added.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();
        }

        public void ViewMembersFlow()
        {
            // to be definied
        }

        public void DeleteMemberFlow()
        {
            // to be definied
        }
    }
    
}