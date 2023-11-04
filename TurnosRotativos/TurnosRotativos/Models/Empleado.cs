using System.ComponentModel.DataAnnotations;

namespace TurnosRotativos.Models
{
    public class Empleado
    {
        public int Id { get; set;}
        public int NroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCreacion { get; set; }
        public virtual List<Jornada> Jornadas { get; set; }
        public Empleado(){}
    }
}
