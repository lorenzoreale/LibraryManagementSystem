using System.Text.Json;
using LMS.Domain.Entities;
using LMS.Domain.Interfaces;

namespace LMS.Infrastructure.Repositories
{
    public class JsonBookRepository : IBookRepository
    {
        private readonly string _filepath = "library_data.json";

        public JsonBookRepository()
        {
            if(!File.Exists(_filepath))
                File.WriteAllText(_filepath, "[]");
        }

        public void Add(Book book)
        {
            var books = GetAll();

            books.Add(book);

            var jsonText = JsonSerializer.Serialize(books);
            File.WriteAllText(_filepath, jsonText);
        }

        public List<Book> GetAll()
        {
            var jsonText = File.ReadAllText(_filepath);

            return JsonSerializer.Deserialize<List<Book>>(jsonText) ?? new List<Book>();
        }

        public void Delete(Guid id)
        {
            var books = GetAll();
            var bookToRemove = books.FirstOrDefault(b => b.Id == id);

            if (bookToRemove == null)
                throw new InvalidOperationException("Book not found.");

            books.Remove(bookToRemove);

            var jsonText = JsonSerializer.Serialize(books);
            File.WriteAllText(_filepath, jsonText);

        }

        public Book? GetById(Guid id)
        {
            var books = GetAll();
            return books.FirstOrDefault(b => b.Id == id);
        }

    }

}