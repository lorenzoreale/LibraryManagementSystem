using LMS.Domain.Enums;
using LMS.Domain.Interfaces;

namespace LMS.Presentation.UI
{
    public class TransactionUI
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly IBookAvailabilityService _availabilityService;
        private readonly ICheckoutService _checkoutService;
        private readonly IReturnService _returnService;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionUI(
            IBookRepository bookRepo,
            IMemberRepository memberRepo,
            IBookAvailabilityService availabilityService,
            ICheckoutService checkoutService,
            IReturnService returnService,
            ITransactionRepository transactionRepo)
        {
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
            _availabilityService = availabilityService;
            _checkoutService = checkoutService;
            _returnService = returnService;
            _transactionRepo = transactionRepo;
        }

        public void CheckoutFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Checkout Book =====");

            var books = _bookRepo.GetAll();

            if (books.Count == 0)
            {
                Console.WriteLine("The library is empty.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            foreach (var book in books)
            {
                int available = _availabilityService.GetAvailableCopies(book.Id);
                Console.WriteLine($"- ID: {book.Id}");
                Console.WriteLine($"  Title: {book.Title}");
                Console.WriteLine($"  Author: {book.Author}");
                Console.WriteLine($"  Available: {available}/{book.Quantity}");
                Console.WriteLine("  -------------------------");
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

            Console.Write("\nInsert the ID of the member: ");
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
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress a key to go back to the menu.");
            Console.ReadKey();
        }

        public void ReturnFlow()
        {
            Console.Clear();
            Console.WriteLine("===== Return Book =====");

            var members = _memberRepo.GetAll();

            if (members.Count == 0)
            {
                Console.WriteLine("No members found.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("--- Members ---");
            foreach (var member in members)
            {
                Console.WriteLine($"- ID: {member.Id}");
                Console.WriteLine($"  Name: {member.Name} {member.Surname}");
                Console.WriteLine("  -------------------------");
            }

            Console.Write("\nInsert the Member ID: ");
            string memberIdInput = Console.ReadLine() ?? "";

            if (!Guid.TryParse(memberIdInput, out Guid memberId))
            {
                Console.WriteLine("\nID not valid.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            var memberTransactions = _transactionRepo.GetByMemberId(memberId);

            var activeBorrows = memberTransactions
                .Where(t => t.BookId != Guid.Empty &&
                            t.Type == TransactionType.Borrow &&
                            !memberTransactions.Any(r =>
                                r.BookId == t.BookId &&
                                r.Type == TransactionType.Return &&
                                r.OccurredAt > t.OccurredAt))
                .ToList();

            if (activeBorrows.Count == 0)
            {
                Console.WriteLine("\nThis member has no active loans.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n--- Active loans ---");
            foreach (var borrow in activeBorrows)
            {
                var book = _bookRepo.GetById(borrow.BookId);
                string bookTitle = book?.Title ?? "Unknown title";
                Console.WriteLine($"- Book ID: {borrow.BookId}");
                Console.WriteLine($"  Title: {bookTitle}");
                Console.WriteLine($"  Borrowed on: {borrow.OccurredAt:dd/MM/yyyy HH:mm}");
                Console.WriteLine("  -------------------------");
            }

            Console.Write("\nInsert the ID of the book to return: ");
            string bookIdInput = Console.ReadLine() ?? "";

            if (!Guid.TryParse(bookIdInput, out Guid bookId))
            {
                Console.WriteLine("\nID not valid.");
                Console.WriteLine("\nPress a key to go back to the menu.");
                Console.ReadKey();
                return;
            }

            try
            {
                _returnService.ReturnBook(memberId, bookId);
                Console.WriteLine("\nBook returned successfully.");
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