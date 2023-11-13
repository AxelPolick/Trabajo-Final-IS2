namespace PF_Ingenieria_Software_II.Services
{
    public interface IUnitOfWork
    {
        IPaypalServices PaypalServices { get; }
    }
}
