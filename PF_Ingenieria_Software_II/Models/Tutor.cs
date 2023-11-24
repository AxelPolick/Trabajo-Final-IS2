using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Tutor
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ContratoId { get; set; }

    public virtual ICollection<Bloque> Bloques { get; set; } = new List<Bloque>();

    public virtual Contrato Contrato { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
