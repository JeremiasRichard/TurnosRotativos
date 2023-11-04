using Microsoft.AspNetCore.Mvc;
using TurnosRotativos.DTOs;
using TurnosRotativos.Helpers;
using TurnosRotativos.Models;
using TurnosRotativos.Services;

namespace TurnosRotativos.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class JornadaController : Controller
    {
        private readonly IJornadaService _jornadaService;

        public JornadaController(IJornadaService jornadaService = null)
        {
            _jornadaService = jornadaService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<JornadaCreacionMuestraDTO> AltaJornada([FromQuery] JornadaCreacionDTO jornada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Por favor revise sus campos!");
            }

            return Ok(_jornadaService.CrearJornada(jornada));
        }

        [HttpGet("Fecha o DNI")]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<List<JornadaMuestraDTO>>>ObtenerPorFechaODni([FromQuery] PaginacionDTO?paginationDTO, string fecha, int?dni)
        {
            var queryable = _jornadaService.GetQueryable();
            await HttpContext.InsertarParametrosEnCabecera(queryable, paginationDTO.Paginas, paginationDTO.registrosPorPagina);
            return await _jornadaService.ObtenerJornadasPorFechaODNI(paginationDTO,fecha,dni);
        }
    }
}
