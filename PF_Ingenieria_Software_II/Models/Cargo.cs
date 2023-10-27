using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Cargo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
