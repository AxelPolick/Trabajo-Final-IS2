using PayPal.Api;
namespace PF_Ingenieria_Software_II.Services
{
    public interface IPaypalServices
    {
        Task<Payment> CreateOrderAysnc(decimal amount, string returnUrl, string cancelUrl);
    }
}
