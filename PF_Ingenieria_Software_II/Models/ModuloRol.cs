using System;
using System.Collections.Generic;

namespace PF_Ingenieria_Software_II.Models;

public partial class ModuloRol
{
    public int ModuloRolId { get; set; }

    public int ModuloId { get; set; }

    public int RolId { get; set; }

    public virtual Modulo Modulo { get; set; } = null!;

    public virtual Rol Rol { get; set; } = null!;
}
