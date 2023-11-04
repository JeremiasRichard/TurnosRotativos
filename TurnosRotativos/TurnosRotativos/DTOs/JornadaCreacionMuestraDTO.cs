namespace TurnosRotativos.DTOs
{
    public class JornadaCreacionMuestraDTO
    {
        public int Id { get; set; }
        public int NroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string Fecha { get; set; }
        public string Concepto { get; set; }  
        public int HsTrabajadas { get; set; }
    }
}
