using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
