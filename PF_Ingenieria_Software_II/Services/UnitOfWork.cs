namespace PF_Ingenieria_Software_II.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration) 
        {
            _configuration = configuration;
            PaypalServices = new PayPalServices(_configuration);
        }
        public IPaypalServices PaypalServices { get; private set; }
    }
}
