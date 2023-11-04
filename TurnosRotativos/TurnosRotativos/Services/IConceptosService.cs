using Microsoft.AspNetCore.Mvc;
using TurnosRotativos.DTOs;
using TurnosRotativos.Models;

namespace TurnosRotativos.Services
{
    public interface IConceptosService
    {
        public Task<ActionResult<List<Concepto>>> ObternerConceptos(PaginacionDTO paginationDTO);
        public IQueryable<Concepto> GetQueryable();
        public Concepto ObtenerPorId(int IdConcepto);
    }
}
