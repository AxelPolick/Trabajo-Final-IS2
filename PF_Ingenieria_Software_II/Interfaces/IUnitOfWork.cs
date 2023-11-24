namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IUnitOfWork
    {
        IPaypalServices PaypalServices { get; }
    }
}
