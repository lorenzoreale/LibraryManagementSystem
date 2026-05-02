using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class Book
    {
        public Guid Id {get; private set; }
        public string Title {get; private set; }
        public string Author {get; private set; }
        public bool isAvailable {get; private set; }

        public Book(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("Title can't be blank.", nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentNullException("Author can't be blank.", nameof(author));
            
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            isAvailable = true;
        }
        [JsonConstructor]
        private Book(Guid id, string title, string author, bool isAvailable)
        {
            Id= id;
            Title = title;
            Author = author;
            this.isAvailable = isAvailable;
        }
        public void CheckOut()
        {
            if (!isAvailable)
                throw new InvalidOperationException($"The book '{Title}' is not available.");
            
            isAvailable = false;
        }

        public void Return()
        {
            isAvailable = true;
        }

    }

}