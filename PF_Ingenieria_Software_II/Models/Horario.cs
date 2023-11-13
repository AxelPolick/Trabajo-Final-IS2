using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Horario
{
    public int Id { get; set; }

    public virtual ICollection<Bloque> Bloques { get; set; } = new List<Bloque>();
}
