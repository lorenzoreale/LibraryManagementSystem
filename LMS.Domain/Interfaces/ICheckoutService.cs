namespace LMS.Domain.Interfaces
{
    public interface ICheckoutService
    {
        void CheckoutBook(Guid memberId, Guid bookId);
    }
}