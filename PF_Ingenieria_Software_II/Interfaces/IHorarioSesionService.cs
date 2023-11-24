namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IHorarioSesionService
    {
        public List<HorarioSesion> FindSesionesPorHorario(int horarioId);

        public int GetHorarioId(int sesionId);
    }
}
