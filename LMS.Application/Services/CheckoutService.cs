using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Domain.Interfaces;

namespace LMS.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IBookAvailabilityService _availabilityService;

        public CheckoutService(IBookRepository bookRepo, IMemberRepository memberRepo, ITransactionRepository transactionRepo, IBookAvailabilityService availabilityService)
        {
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
            _transactionRepo = transactionRepo;
            _availabilityService = availabilityService;
        }
        
        public void CheckoutBook(Guid memberId, Guid bookId)
{
        var book = _bookRepo.GetById(bookId);
        if (book == null)
            throw new InvalidOperationException("No book found.");

        int availableCopies = _availabilityService.GetAvailableCopies(bookId);
        if (availableCopies <= 0)
            throw new InvalidOperationException("No copies available.");

        var member = _memberRepo.GetById(memberId);
        if (member == null)
            throw new InvalidOperationException("Member not found.");

        var memberTransactions = _transactionRepo.GetByMemberId(memberId);
        var activeBorrowCount = memberTransactions
            .Count(t => t.Type == TransactionType.Borrow &&
                        !memberTransactions.Any(r =>
                            r.BookId == t.BookId &&
                            r.Type == TransactionType.Return &&
                            r.OccurredAt > t.OccurredAt));

        if (activeBorrowCount >= 5)
            throw new InvalidOperationException("Member has reached the maximum of 5 active loans.");

        Transaction newBorrow = new Transaction(memberId, bookId, TransactionType.Borrow);
        _transactionRepo.Add(newBorrow);
        }

    }
}