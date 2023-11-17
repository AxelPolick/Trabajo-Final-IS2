using PayPal.Api;
using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Services
{
    public class PayPalServices : IPaypalServices
    {
        private readonly APIContext apiContext;
        private readonly Payment payment;
        private readonly IConfiguration configuration;

        public PayPalServices(IConfiguration configuration)
        {
            this.configuration = configuration;

            var clientId = configuration["PayPal:ClientId"];
            var clienSecret = configuration["PayPal:ClientSecret"];

            var config = new Dictionary<string, string>
            {
                { "mode", "sandbox" },
                { "clientId", clientId},
                { "clientSecret", clienSecret}
            };

            var accesToken = new OAuthTokenCredential(clientId, clienSecret, config).GetAccessToken();
            apiContext = new APIContext(accesToken);

            payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal"}
            };
        }

        public async Task<Payment> CreateOrderAysnc(decimal amount, string returnUrl, string cancelUrl)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(configuration["PayPal:ClientId"], configuration["PayPal:ClientSecret"]).GetAccessToken());

            var itemList = new ItemList()
            {
                items = new List<Item>()
                {
                    new Item()
                    {
                        name = "Membership Fee",
                        currency = "USD",
                        price = amount.ToString("0.00"),
                        quantity = "1",
                        sku = "membership"
                    }
                }
            };

            var transaction = new Transaction()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = amount.ToString("0,00"),
                    details = new Details()
                    {
                        subtotal = amount.ToString("0.00")
                    }
                },
                item_list = itemList,
                description = "MembershipFee"
            };

            var payment = new Payment()
            {
                intent = "sale",
                payer = new Payer() { payment_method = "paypal" },
                redirect_urls = new RedirectUrls()
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                },
                transactions = new List<Transaction>() { transaction }
            };

            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }
    }
}
