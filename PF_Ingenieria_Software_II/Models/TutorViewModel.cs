using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PF_Ingenieria_Software_II.Models
{
    public class TutorViewModel
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

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

        public string? NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El campo CONTRASEÑA es obligatorio")]
        [StringLength(8, ErrorMessage = "La clave debe contener 8 caracteres")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;

        [NotMapped]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmarContrasena { get; set; }

        [Required(ErrorMessage = "El campo NroDocumento es obligatorio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo NroDocumento solo debe contener números")]
        public string NroDocumento { get; set; } = null!;

        public int? DocumentoId { get; set; }

        public int? RolId { get; set; }

        public int? EstadoId { get; set; }

        public string? NombreRol { get; set; }

        public string? NombreDocumento { get; set; }

        //CONTRATO

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaFin { get; set; }

        public decimal? Salario { get; set; }

        public int? CargoId { get; set; }

        public string? NombreCargo { get; set; }

        [Required(ErrorMessage = "Selecciona al menos una disciplina.")]
        public List<int>? DisciplinasSeleccionadas { get; set; }

        public List<Disciplina> Disciplinas { get; set; }

        public TutorViewModel()
        {
            Disciplinas = new List<Disciplina>();
        }
    }
}
