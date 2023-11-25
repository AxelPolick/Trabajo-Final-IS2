namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface ITutorService
    {
        Tutor FindTutor(int idUsuario);

        Tutor FindTutorTutorId(int idTutor);

        int GetTutorId(int idUsuario);

        int Size();
    }
}
