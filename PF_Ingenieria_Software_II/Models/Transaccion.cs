using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Monto { get; set; }

    public decimal? NumeroAutenticacion { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
