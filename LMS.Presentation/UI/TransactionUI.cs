using LMS.Domain.Interfaces;
using LMS.Application.Interfaces;

namespace LMS.Presentation.UI
{
    public class TransactionUI
    {
        private readonly IBookRepository _bookRepo;
        private readonly IBookAvailabilityService _availabilityService;
        private readonly ICheckoutService _checkoutService;

        public TransactionUI(IBookRepository bookRepo, IBookAvailabilityService availabilityService, ICheckoutService checkoutServe)
        {
            _bookRepo = bookRepo;
            _availabilityService = availabilityService;
            _checkoutService = checkoutServe;
        }

        public void CheckoutFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Checkout Book =====");

            var books = _bookRepo.GetAll();

            if(books.Count == 0)
            {
                Console.WriteLine("The library is empty.");
                Console.WriteLine("Press a key to go back to the menu.");
                Console.ReadKey();
                return;
            }
            else
            {
                foreach (var book in books)
                {
                    int available = _availabilityService.GetAvailableCopies(book.Id);
                    Console.WriteLine($"- ID: {book.Id}");
                    Console.WriteLine($"  Title: {book.Title}");
                    Console.WriteLine($"  Author: {book.Author}");
                    Console.WriteLine($"  Available: {available}/{book.Quantity}");
                    Console.WriteLine("  -------------------------");
                }
            }

            Console.Write("\nInsert the ID of the book to checkout: ");
            string bookIdToCheckout = Console.ReadLine() ?? "";

            if (!Guid.TryParse(bookIdToCheckout, out Guid bookId))
            {
                Console.WriteLine("\nID not valid.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            Console.Write("\nInsert the ID of the member to checkout: ");
            string memberIdToCheckout = Console.ReadLine() ?? "";
            
            if (!Guid.TryParse(memberIdToCheckout, out Guid memberId))
            {
                Console.WriteLine("\nID not valid.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            try
            {
                _checkoutService.CheckoutBook(memberId, bookId);
                Console.WriteLine("\nCheckout successful.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n Error: {ex.Message}");
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();
        }
    }
}