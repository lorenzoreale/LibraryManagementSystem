using LMS.Application.Interfaces;

namespace LMS.Application.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IBookAvailabilityService _availabilityService;

        public ReturnService(IBookRepository bookRepo, IMemberRepository memberRepo, ITransactionRepository transactionRepo, IBookAvailabilityService availabilityService)
        {
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
            _transactionRepo = transactionRepo;
            _availabilityService = availabilityService;
        }

        public void ReturnBook(Guid memberId, Guid bookId)
        {
            // to be definied
        }
    }
}