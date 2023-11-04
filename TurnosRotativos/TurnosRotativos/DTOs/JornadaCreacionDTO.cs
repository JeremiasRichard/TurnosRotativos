using TurnosRotativos.Models;

namespace TurnosRotativos.DTOs
{
    public class JornadaCreacionDTO
    {
        public int IdEmpleado { get; set; }
        public string Fecha { get; set; }
        public int IdConcepto { get; set; }
        public int? HorasTrabajadas { get; set; }
    }

}
