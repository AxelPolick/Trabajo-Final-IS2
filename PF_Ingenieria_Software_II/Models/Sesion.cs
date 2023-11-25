using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Sesion
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public DateTime? HoraInicio { get; set; }

    public DateTime? HoraFin { get; set; }
}
