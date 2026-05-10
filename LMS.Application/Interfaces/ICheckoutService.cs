namespace LMS.Application.Interfaces
{
    public interface ICheckoutService
    {
        void CheckoutBook(Guid memberId, Guid bookId);
    }
}