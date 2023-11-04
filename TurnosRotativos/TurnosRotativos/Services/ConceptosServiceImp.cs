using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosRotativos.DTOs;
using TurnosRotativos.Helpers;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Models;

namespace TurnosRotativos.Services
{
    public class ConceptosServiceImp : IConceptosService
    {
        private readonly IConceptosRepository _conceptoRepository;

        public ConceptosServiceImp(IConceptosRepository conceptoRepository)
        {
            _conceptoRepository = conceptoRepository;
        }

        public async Task<ActionResult<List<Concepto>>> ObternerConceptos(PaginacionDTO paginacionDTO)
        {
            var conceptos = await _conceptoRepository.ObtenerConceptos().ToPaginate(paginacionDTO).ToListAsync();
            return conceptos;
        }

        public IQueryable<Concepto> GetQueryable()
        {
            return _conceptoRepository.ObtenerConceptos();
        }

        public Concepto ObtenerPorId(int IdConcepto)
        {
            return _conceptoRepository.ObtenerConceptoPorId(IdConcepto);
        }
    }
}
