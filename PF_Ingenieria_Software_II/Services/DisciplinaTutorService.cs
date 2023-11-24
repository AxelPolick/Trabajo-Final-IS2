using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Services
{
    public class DisciplinaTutorService: IDisciplinaTutorService
    {
        private readonly PolideportivobdContext _context;

        public DisciplinaTutorService(PolideportivobdContext context)
        {
            _context = context;
        }

        public List<TutorDisciplina> FindTutorDisciplina(int idTutor)
        {
            var listaDisciplinas = (from Tabla in _context.TutorDisciplinas
                                    where Tabla.TutorId == idTutor
                                    select Tabla).ToList();
            return listaDisciplinas;
        }
    }
}
