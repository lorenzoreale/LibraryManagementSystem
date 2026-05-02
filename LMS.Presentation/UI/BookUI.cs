using System;
using LMS.Domain.Entities;
using LMS.Domain.Interfaces;

namespace LMS.Presentation.UI
{
    public class BookUI
    {
        private readonly IBookRepository _bookRepo;

        public BookUI(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        // FIXED: Notice there is NO 'static' keyword here
        public void AddBookFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Add a new book =====");

            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Author: ");
            string author = Console.ReadLine() ?? "";

            try
            {
                Book newBook = new Book(title, author);
                _bookRepo.Add(newBook);
                Console.WriteLine($"\nOperation Successful. The book '{newBook.Title}' has been added.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"\nValidation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();
        }

        // FIXED: Notice there is NO 'static' keyword here
        public void ViewBooksFlow()
        {
            Console.Clear();
            Console.WriteLine("===== View Books =====");

            var books = _bookRepo.GetAll();

            if (books.Count == 0)
            {
                Console.WriteLine("The library is currently empty.");
            }
            else
            {
                foreach (var book in books)
                {
                    string status = book.isAvailable ? "Available" : "Checked Out";
                    Console.WriteLine($"- ID: {book.Id}");
                    Console.WriteLine($"  Title: {book.Title}");
                    Console.WriteLine($"  Author: {book.Author}");
                    Console.WriteLine($"  Status: [{status}]");
                    Console.WriteLine("  -------------------------");
                }
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();
        }
    }
}