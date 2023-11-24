using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class HorarioSesion
{
    public int HorarioSesionId { get; set; }

    public int HorarioId { get; set; }

    public int SesionId { get; set; }

    public virtual Horario Horario { get; set; } = null!;

    public virtual Sesion Sesion { get; set; } = null!;
}
