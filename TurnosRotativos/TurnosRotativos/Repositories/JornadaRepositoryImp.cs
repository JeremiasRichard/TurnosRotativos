using Microsoft.EntityFrameworkCore;
using TurnosRotativos.Database;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Models;

namespace TurnosRotativos.Repositories
{
    public class JornadaRepositoryImp : IJornadaRepository
    {
        private readonly ApplicationDbContext _context;

        public JornadaRepositoryImp(ApplicationDbContext context)
        {
            _context = context;
        }

        public Jornada CrearJornada(Jornada jornada)
        {
            _context.Jornadas.Add(jornada);
            return jornada;
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }

        public IQueryable<Jornada> ObtenerJornadas()
        {
           return _context.Jornadas.AsQueryable()
                .Include(c => c.Concepto)
                .Include(c => c.Empleado);
        }
        public Jornada ObtenerPorDNI(int dni)
        {
            return _context.Jornadas.Where(x => x.IdEmpleado == dni).FirstOrDefault();
        }
        public Jornada ObtenerPorFecha(string fecha)
        {
            return _context.Jornadas.Where(x => x.Fecha.Equals(fecha, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
