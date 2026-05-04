using System.Text.Json;
using LMS.Domain.Entities;
using LMS.Domain.Interfaces;

namespace LMS.Infrastructure.Repositories
{
    public class JsonTransactionRepository : ITransactionRepository
    {
        private readonly string _filepath = "transactions_data.json";

        public JsonTransactionRepository()
        {
            if (!File.Exists(_filepath))
                File.WriteAllText(_filepath,"[]");
        }

        public void Add(Transaction transaction)
        {
            var transactions = GetAll();
            
            transactions.Add(transaction);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonText = JsonSerializer.Serialize(transactions, options);

            File.WriteAllText(_filepath,jsonText);
        }

        public List<Transaction> GetAll()
        {
            var jsonText = File.ReadAllText(_filepath);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Transaction>>(jsonText, options) ?? new List<Transaction>();
        }

        public List<Transaction> GetByBookId(Guid bookId)
        {
            var transactions = GetAll();
            return transactions.Where(t => t.BookId == bookId).ToList();
        }

        public List<Transaction> GetByMemberId(Guid memberId)
        {
            var transactions = GetAll();
            return transactions.Where(t => t.MemberId == memberId).ToList();
        }
    }
}