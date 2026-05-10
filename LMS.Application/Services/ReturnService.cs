using LMS.Domain.Enums;
using LMS.Domain.Entities;
using LMS.Domain.Interfaces;

namespace LMS.Application.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ITransactionRepository _transactionRepo;

        public ReturnService(IBookRepository bookRepo, IMemberRepository memberRepo, ITransactionRepository transactionRepo)
        {
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
            _transactionRepo = transactionRepo;
        }

        public void ReturnBook(Guid memberId, Guid bookId)
        {
            var book = _bookRepo.GetById(bookId);
            if (book == null)
                throw new InvalidOperationException("No book found.");

            var member = _memberRepo.GetById(memberId);
            if (member == null)
                throw new InvalidOperationException("Member not found.");

            var memberTransactions = _transactionRepo.GetByMemberId(memberId);

            bool hasActiveBorrow = memberTransactions
                .Any(t => t.BookId == bookId &&
                t.Type == TransactionType.Borrow &&
                !memberTransactions.Any(r => 
                r.BookId == bookId &&
                r.Type == TransactionType.Return &&
                r.OccurredAt > t.OccurredAt));

            if (!hasActiveBorrow)
                throw new InvalidOperationException("No active loan found for this member on this book.");

            Transaction newReturn = new Transaction(memberId, bookId, TransactionType.Return);
            _transactionRepo.Add(newReturn);

        }
    }
}