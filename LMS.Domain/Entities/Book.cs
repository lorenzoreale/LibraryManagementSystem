using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class Book
    {
        public Guid Id {get; private set; }
        public string Title {get; private set; }
        public string Author {get; private set; }
        public int Quantity {get; private set; }

        public Book(string title, string author, int quantity)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title), "Title can't be blank.");
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentNullException(nameof(author), "Author can't be blank.");
            if (quantity <=0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            Quantity = quantity;
        }
        [JsonConstructor]
        private Book(Guid id, string title, string author, int quantity)
        {
            Id= id;
            Title = title;
            Author = author;
            Quantity = quantity;
        }
    }

}