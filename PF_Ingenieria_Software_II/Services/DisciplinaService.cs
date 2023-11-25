using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Services
{
    public class DisciplinaService: IDisciplinaService
    {
        private readonly PolideportivobdContext _context;

        public DisciplinaService(PolideportivobdContext context)
        {
            _context = context;
        }

        public Disciplina FindDisciplina(int id) 
        { 
            var Disciplina = (from Table in _context.Disciplinas
                              where Table.Id == id
                              select Table).FirstOrDefault();

            if (Disciplina == null)
            {
                return new Disciplina();
            }

            return Disciplina;
        }

        public int GetDisciplinaId(string nombre)
        {
            var disciplina = (from TablaDisciplina in _context.Disciplinas
                            where TablaDisciplina.Nombre == nombre
                            select TablaDisciplina).FirstOrDefault();

            if (disciplina == null)
            {
                return 1;
            }

            return disciplina.Id;
        }

        public int Size()
        {
            return _context.Disciplinas.Count() + 1;
        }
    }
}
