using Microsoft.AspNetCore.Mvc;
using TurnosRotativos.DTOs;
using TurnosRotativos.Models;

namespace TurnosRotativos.Services
{
    public interface IJornadaService
    {
        public JornadaCreacionMuestraDTO CrearJornada(JornadaCreacionDTO jornada);
        public IQueryable<Jornada> GetQueryable();
        public Task<ActionResult<List<JornadaMuestraDTO>>> ObtenerJornadasPorFechaODNI(PaginacionDTO paginacionDTO, string fecha, int ? dni);
    }
}

