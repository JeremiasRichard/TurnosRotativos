
namespace TurnosRotativos.Models
{
    public class Concepto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ?HsMinimo { get; set; }
        public int ? HsMaximo { get; set; }
        public bool Laborable { get; set; }
        public Concepto() {}
    }
}
