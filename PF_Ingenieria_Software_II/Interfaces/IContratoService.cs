namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IContratoService
    {
        public Contrato FindContrato(int contratoId);
        public int Size();
    }
}
