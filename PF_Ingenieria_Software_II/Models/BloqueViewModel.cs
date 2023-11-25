using System.ComponentModel.DataAnnotations;

namespace PF_Ingenieria_Software_II.Models
{
    public class BloqueViewModel
    {
        public int Id { get; set; }

        public string? NombreDisciplina { get; set; }

        public string? ApellidoTutor { get; set; }

        public string? Ubicacion { get; set; }

        public float? Precio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "El número de semanas debe ser mayor a cero.")]
        public int NumeroSemanas { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "Selecciona al menos un dia de la semana.")]
        public List<DayOfWeek> DiasSeleccionados { get; set; }

        public List<DayOfWeek> Dias { get; set; }

        public BloqueViewModel()
        {
            Dias = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            };
        }
    }
}
