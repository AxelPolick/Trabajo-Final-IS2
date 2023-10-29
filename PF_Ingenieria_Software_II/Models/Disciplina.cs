using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Disciplina
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Bloque> Bloques { get; set; } = new List<Bloque>();
}
