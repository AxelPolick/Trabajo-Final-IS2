using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class TutorDisciplina
{
    public int DisciplinaId { get; set; }

    public int TutorId { get; set; }

    public virtual Disciplina Disciplina { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;
}
