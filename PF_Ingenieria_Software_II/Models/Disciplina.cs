using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PF_Ingenieria_Software_II.Models;

public partial class Disciplina
{
    public int Id { get; set; }

    [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "El nombre debe contener solo letras.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(50, ErrorMessage = "La descripción no puede superar los 50 caracteres.")]
    public string? Descripcion { get; set; }

    public virtual ICollection<Bloque> Bloques { get; set; } = new List<Bloque>();
}
