using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF_Ingenieria_Software_II.Models;

public partial class Cargo
{
    public int Id { get; set; }

    [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "El nombre debe contener solo letras.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(50, ErrorMessage = "La descripción no puede superar los 50 caracteres.")]
    public string? Descripcion { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
