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
            Console.Clear();
            Console.WriteLine("===== View Members =====");

            var members = _memberRepo.GetAll();

            if (members.Count == 0)
            {
                Console.WriteLine("No members found.");
            }
            else
            {
                foreach (var member in members)
                {
                    Console.WriteLine($"- ID: {member.Id}");
                    Console.WriteLine($"  Name: {member.Name}");
                    Console.WriteLine($"  Surname: {member.Surname}");
                    Console.WriteLine($"  Email: {member.Email}");
                    Console.WriteLine($"  Date of Birth: {member.DateOfBirth}");
                    Console.WriteLine("  -------------------------");
                }
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();
        }

        public void DeleteMemberFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Delete Member =====");

            var members = _memberRepo.GetAll();

            if (members.Count == 0)
            {
                Console.WriteLine("No members found.");
                Console.WriteLine("Press a key to go back to the menu");
                Console.ReadKey();
                return;
            }

            foreach (var member in members)
            {
                    Console.WriteLine($"- ID: {member.Id}");
                    Console.WriteLine($"  Name: {member.Name}");
                    Console.WriteLine($"  Surname: {member.Surname}");
                    Console.WriteLine($"  Email: {member.Email}");
                    Console.WriteLine($"  Date of Birth: {member.DateOfBirth}");
                    Console.WriteLine("  -------------------------");                
            }

            Console.Write("\nInsert the ID of the member to delete: ");
            
            string memberIdToRemove = Console.ReadLine() ?? "";

            if (!Guid.TryParse(memberIdToRemove, out Guid id))
            {
                Console.WriteLine("\nID not valid.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            try
            {
                _memberRepo.Delete(id);
                Console.WriteLine("\nMember successfully deleted.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();

        }
    }
    
}