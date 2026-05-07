namespace LMS.Domain.Interfaces
{
    public interface IBookAvailabilityService
    {
        int GetAvailableCopies(Guid bookId);
    }
}