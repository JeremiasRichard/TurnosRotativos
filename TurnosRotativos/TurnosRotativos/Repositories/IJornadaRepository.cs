using TurnosRotativos.Models;

namespace TurnosRotativos.Interfaces
{
    public interface IJornadaRepository
    {
        public Jornada CrearJornada(Jornada jornada);
        public void Guardar();
        public IQueryable<Jornada> ObtenerJornadas();
        public Jornada ObtenerPorFecha(string fecha);
        public Jornada ObtenerPorDNI(int dni);
    }
}
