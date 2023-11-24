namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IDisciplinaService
    {
        public Disciplina FindDisciplina(int id);

        public int GetDisciplinaId(string nombre);

        public int Size();
    }
}
