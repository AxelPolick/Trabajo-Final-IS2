using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Services
{
    public class ContratoService: IContratoService
    {
        private readonly PolideportivobdContext _context;

        public ContratoService(PolideportivobdContext context)
        {
            _context = context;
        }
        public Contrato FindContrato(int contratoId)
        {
            var contrato = (from Tabla in _context.Contratos
                            where Tabla.Id == contratoId
                            select Tabla).FirstOrDefault();

            if(contrato == null) 
            {
                return new Contrato();
            }

            return contrato;
        }

        public int Size()
        { 
            return _context.Contratos.Count() + 1;
        }
    }
}
