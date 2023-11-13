namespace PF_Ingenieria_Software_II.Interfaces
{
    public interface IDocumentTypeService
    {
        TipoDocumento FindDocumentType(int id);

        int GetDocumentId(string name);
    }
}
