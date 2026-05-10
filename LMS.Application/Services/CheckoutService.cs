using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Domain.Interfaces;
using LMS.Application.Interfaces;

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

            // case of member having >5 books borrowed

            Transaction newBorrow = new Transaction(memberId, bookId, TransactionType.Borrow);
            _transactionRepo.Add(newBorrow);

        }

    }
}