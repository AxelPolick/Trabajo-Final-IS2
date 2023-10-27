using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF_Ingenieria_Software_II.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombres { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? Edad { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El campo EMAIL es obligatorio")]
    [EmailAddress]
    public string Correo { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    [Required(ErrorMessage = "El campo CONTRASEÑA es obligatorio")]
    [StringLength(8, ErrorMessage = "La clave debe contener 8 caracteres")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    public string NroDocumento { get; set; } = null!;

    public int DocumentoId { get; set; }

    public int EstadoId { get; set; }

    public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();

    public virtual TipoDocumento Documento { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();
}
