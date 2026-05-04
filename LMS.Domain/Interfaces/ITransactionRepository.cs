using LMS.Domain.Entities;

namespace LMS.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        
        List<Transaction> GetAll();

        List<Transaction> GetByBookId(Guid bookId);

        List<Transaction> GetByMemberId(Guid memberId); // are all these methods necessary? an intelligence team will have access to a flat table (json in this case) with all the transactions log
    }
}