using System.Security.Claims;

namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IUsuarioService
    {
        Usuario ObtenerUsuarioActual();

        Usuario FindUsuario(int id);

        int GetUsuarioId(string apellidoPaterno);

        int Size();
    }
}
