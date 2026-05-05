using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class Member
    {
        public Guid Id {get; private set; }
        public string Name {get; private set; }
        public string Surname {get; private set; }
        public DateOnly DateOfBirth {get; private set; }
        public string Email {get; private set; }
        public DateOnly RegistrationDate {get; private set; }
        // public bool isDeleted {get; private set; } // a member record at least is deactivated, not phisically removed from the system

        public Member(string name, string surname,  string email, DateOnly dateOfBirth)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name can't be blank.");
            if(string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname), "Surname can't be blank.");
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email can't be blank.");
            if(!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException(nameof(email), "Email format is not valid.");
            

            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Email = email;
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
        }

        [JsonConstructor]
        private Member(Guid id, string name, string surname, DateOnly dateOfBirth, string email, DateOnly registrationDate)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Email = email;
            RegistrationDate = registrationDate;
        }
    }
}