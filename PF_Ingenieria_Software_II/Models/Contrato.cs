using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Contrato
{
    public int Id { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public decimal? Salario { get; set; }

    public int CargoId { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();
}
