using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Matricula
{
    public int Id { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int AlumnoId { get; set; }

    public int EstadoId { get; set; }

    public int TransaccionId { get; set; }

    public virtual Alumno Alumno { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Retiro> Retiros { get; set; } = new List<Retiro>();

    public virtual Transaccion Transaccion { get; set; } = null!;
}
