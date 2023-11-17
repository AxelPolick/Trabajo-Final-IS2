using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class Bloque
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int DisciplinaId { get; set; }

    public int HorarioId { get; set; }

    public int TutorId { get; set; }

    public string? Ubicacion { get; set; }

    public virtual Disciplina Disciplina { get; set; } = null!;

    public virtual Horario Horario { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;
}
