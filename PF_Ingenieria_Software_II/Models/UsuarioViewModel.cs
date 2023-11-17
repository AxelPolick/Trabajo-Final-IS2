using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PF_Ingenieria_Software_II.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "El campo Nombres solo debe contener letras")]
        public string? Nombres { get; set; }


        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El campo Apellido Paterno solo debe contener letras")]
        public string? ApellidoPaterno { get; set; }


        [Required(ErrorMessage = "El campo Apellido Materno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El campo Apellido Materno solo debe contener letras")]
        public string? ApellidoMaterno { get; set; }


        [Required(ErrorMessage = "El campo Edad es obligatorio")]
        [Range(0, 150, ErrorMessage = "La Edad debe estar entre 0 y 150")]
        public int? Edad { get; set; }


        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public string? Direccion { get; set; }


        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo Teléfono solo debe contener números")]
        public string? Telefono { get; set; }


        [Required(ErrorMessage = "El campo EMAIL es obligatorio")]
        [EmailAddress]
        public string Correo { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El campo CONTRASEÑA es obligatorio")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;


        [Required(ErrorMessage = "El campo NroDocumento es obligatorio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo NroDocumento solo debe contener números")]
        public string NroDocumento { get; set; } = null!;


        public string? NombreRol { get; set; }

        public string? NombreDocumento { get; set; }
    }
}
