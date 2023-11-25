using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public virtual ICollection<Retiro> Retiros { get; set; } = new List<Retiro>();

    public virtual Usuario Usuario { get; set; } = null!;
}
