using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class BloqueAlumno
{
    public int BloqueAlumnoId { get; set; }

    public int BloqueId { get; set; }

    public int AlumnoId { get; set; }

    public virtual Alumno Alumno { get; set; } = null!;

    public virtual Bloque Bloque { get; set; } = null!;
}
