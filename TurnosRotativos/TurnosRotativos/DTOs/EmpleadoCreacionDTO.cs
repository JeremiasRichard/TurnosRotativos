using System.ComponentModel.DataAnnotations;

namespace TurnosRotativos.DTOs
{
    public class EmpleadoCreacionDTO
    {
        [Required]
        public int NroDocumento { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FechaNacimiento { get; set; }
        [Required]
        public string FechaIngreso { get; set; }
    }
}
