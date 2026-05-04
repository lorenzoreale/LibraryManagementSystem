using LMS.Domain.Entities;

namespace LMS.Domain.Interfaces
{
    public interface IBookRepository
    {
        void Add(Book book);
        
        List<Book> GetAll();

        void Delete(Guid id); // a book record should not be deleted from the system, at least is not available anymore
    }

}