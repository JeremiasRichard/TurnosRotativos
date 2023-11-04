using Microsoft.AspNetCore.Mvc;
using TurnosRotativos.DTOs;
using TurnosRotativos.Helpers;
using TurnosRotativos.Models;
using TurnosRotativos.Services;

namespace TurnosRotativos.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class ConceptosController : Controller
    {
        private readonly IConceptosService _conceptosService;
        public ConceptosController(IConceptosService conceptosService) => _conceptosService = conceptosService;

        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<List<Concepto>>> ObtenerTodosLosConceptos([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = _conceptosService.GetQueryable();
            await HttpContext.InsertarParametrosEnCabecera(queryable, paginacionDTO.Paginas, paginacionDTO.registrosPorPagina);
            return await _conceptosService.ObternerConceptos(paginacionDTO);
        }
    }
}
