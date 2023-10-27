using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Estado
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
