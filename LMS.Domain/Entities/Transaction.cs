using System.Text.Json.Serialization;
using LMS.Domain.Enums;

namespace LMS.Domain.Entities
{    
    public class Transaction
    {
        public Guid Id {get; private set; }
        public Guid MemberId {get; private set; }
        public Guid BookId {get; private set; }
        public TransactionType Type {get; private set; }
        public DateTime OccurredAt {get; private set; }
    
        public Transaction(Guid memberId, Guid bookId, TransactionType type)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            BookId = bookId;
            Type = type;
            OccurredAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        private Transaction(Guid id, Guid memberId, Guid bookId, TransactionType type, DateTime occurredAt)
        {
            Id = id;
            MemberId = memberId;
            BookId = bookId;
            Type = type;
            OccurredAt = occurredAt;
        }
    }
}