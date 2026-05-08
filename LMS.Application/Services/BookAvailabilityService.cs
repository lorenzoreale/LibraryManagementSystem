using LMS.Domain.Enums;
using LMS.Domain.Interfaces;

namespace LMS.Application.Services
{
    public class BookAvailabilityService : IBookAvailabilityService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ITransactionRepository _transactionRepo;

        public BookAvailabilityService(IBookRepository bookRepo, ITransactionRepository transactionRepo)
        {
            _bookRepo = bookRepo;
            _transactionRepo = transactionRepo;
        }

        public int GetAvailableCopies(Guid bookId)
        {
            var book = _bookRepo.GetById(bookId);

            if (book == null)
                throw new InvalidOperationException("Book not found.");

            var transactions = _transactionRepo.GetByBookId(bookId);

            var activeBorrows = transactions
                .Where(t => t.Type == TransactionType.Borrow)
                .Count(borrow => !transactions.Any(t => t.Type == TransactionType.Return &&
                    t.MemberId == borrow.MemberId &&
                    t.OccurredAt > borrow.OccurredAt));

            return book.Quantity - activeBorrows;
        }
    }
}