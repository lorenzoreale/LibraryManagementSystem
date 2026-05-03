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

        public void DeleteBookFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Delete a book =====");

            var books = _bookRepo.GetAll();

            if(books.Count == 0)
            {
                Console.WriteLine("The library is empty.");
                Console.WriteLine("Press a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"- ID: {book.Id}");
                Console.WriteLine($"  Title: {book.Title}");
                Console.WriteLine($"  Author: {book.Author}");
                Console.WriteLine("  -------------------------");
            }

            Console.Write("\nInsert the ID of the book to delete: ");

            string bookIdToRemove = Console.ReadLine() ?? "";

            if (!Guid.TryParse(bookIdToRemove, out Guid id))
            {
                Console.WriteLine("\nID not valid.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            try
            {
                _bookRepo.Delete(id);
                Console.WriteLine("\nBook successfully deleted.");
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