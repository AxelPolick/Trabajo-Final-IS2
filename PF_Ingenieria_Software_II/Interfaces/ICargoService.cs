namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface ICargoService
    {
        public Cargo FindCargo(int id);
        public int Size();

        public int GetCargoId(string nombre);
    }
}
