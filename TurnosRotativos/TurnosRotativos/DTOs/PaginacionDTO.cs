namespace TurnosRotativos.DTOs
{
    public class PaginacionDTO
    {
        public int Paginas { get; set; } = 1;
        private int RegistrosPorPagina = 10;
        private readonly int MaximoPorPagina = 50;

        public int registrosPorPagina
        {
            get
            {
                return RegistrosPorPagina;
            }
            set
            {
                RegistrosPorPagina = (value > MaximoPorPagina) ? RegistrosPorPagina : value;
            }
        }

    }
}
