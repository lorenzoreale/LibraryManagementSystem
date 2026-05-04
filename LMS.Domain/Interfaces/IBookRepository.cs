using LMS.Domain.Entities;

namespace LMS.Domain.Interfaces
{
    public interface IBookRepository
    {
        void Add(Book book);
        
        List<Book> GetAll();

        void Delete(Guid id);
    }

}