namespace LMS.Domain.Interfaces
{
    public interface IReturnService
    {
        void ReturnBook(Guid memberId, Guid bookId);
    }
}