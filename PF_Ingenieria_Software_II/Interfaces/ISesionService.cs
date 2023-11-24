namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface ISesionService
    {
        public List<Sesion> ObtenerSesiones();

        public Sesion FindSesion(int idSesion);
    }
}
