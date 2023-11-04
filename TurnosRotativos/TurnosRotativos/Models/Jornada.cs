namespace TurnosRotativos.Models
{
    public class Jornada
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public virtual Empleado  Empleado { get; set; }
        public string Fecha { get; set; }
        public int IdConcepto { get; set; }
        public virtual Concepto Concepto { get; set; }
        public int ? HorasTrabajadas{ get; set; }
        public Jornada() {}
    }
}
