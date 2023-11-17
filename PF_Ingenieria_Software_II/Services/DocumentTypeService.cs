using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly PolideportivobdContext _context;

        public DocumentTypeService(PolideportivobdContext context)
        {
            _context = context;
        }

        public TipoDocumento FindDocumentType(int id)
        {
            var TipoDocumento = (from DocumentTable in _context.TipoDocumentos
                       where DocumentTable.Id == id
                       select DocumentTable).FirstOrDefault();

            if (TipoDocumento == null)
            {
                return new TipoDocumento();
            }

            return TipoDocumento;
        }

        public int GetDocumentId(string name)
        {
            var document = (from DocumentTable in _context.TipoDocumentos
                                 where DocumentTable.Nombre == name
                                 select DocumentTable).FirstOrDefault();

            if (document == null)
            {
                return 1;
            }

            return document.Id;
        }
    }
}
