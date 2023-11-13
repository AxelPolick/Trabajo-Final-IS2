using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class MatriculaBloque
{
    public int MatriculaId { get; set; }

    public int BloqueId { get; set; }

    public decimal? PrecioCurso { get; set; }

    public int EstadoId { get; set; }

    public virtual Bloque Bloque { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual Matricula Matricula { get; set; } = null!;
}
