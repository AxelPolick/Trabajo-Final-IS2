using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_Ingenieria_Software_II.Models;

public partial class TutorDisciplina
{
    public int TutorDisciplinaId { get; set; }

    public int DisciplinaId { get; set; }

    public int TutorId { get; set; }

    public virtual Disciplina Disciplina { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;
}
