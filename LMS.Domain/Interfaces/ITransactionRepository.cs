using LMS.Domain.Entities;

namespace LMS.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        
        List<Transaction> GetAll();

        List<Transaction> GetByBookId(Guid bookId);

        List<Transaction> GetByMemberId(Guid memberId);
    }
}