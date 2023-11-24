using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Retiro
{
    public int Id { get; set; }

    public int AlumnoId { get; set; }

    public int MatriculaId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Motivo { get; set; }

    public virtual Alumno Alumno { get; set; } = null!;

    public virtual Matricula Matricula { get; set; } = null!;
}
