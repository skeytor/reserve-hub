namespace ReserveHub.Application.Providers;

public interface IPaymentProvider
{
    Task<bool> ProcessPayment(decimal amount, string currency, string paymentMethod, string userId);
}
