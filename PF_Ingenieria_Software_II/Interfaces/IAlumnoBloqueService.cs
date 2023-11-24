namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IAlumnoBloqueService
    {
        public List<BloqueAlumno> FindBloquesPorAlumno(int idAlumno);

        public List<BloqueAlumno> FindAlumnosPorBloque(int idBloque);

        public List<Alumno> GetAlumnos(List<BloqueAlumno> lstBloqueAlumno);

        public List<Bloque> GetBloques(List<BloqueAlumno> lstBloqueAlumno);
    }
}
