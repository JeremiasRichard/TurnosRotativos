using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnosRotativos.DTOs;
using TurnosRotativos.Exceptions;
using TurnosRotativos.Helpers;
using TurnosRotativos.Models;
using TurnosRotativos.Services;
using TurnosRotativos.Validations;

namespace TurnosRotativos.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public ActionResult<EmpleadoMuestraDTO> AltaEmpleado([FromQuery] EmpleadoCreacionDTO empleado)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Por favor revise sus campos!");
            }

            return  Ok( _empleadoService.AltaEmpleado(empleado));
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<List<EmpleadoMuestraDTO>>> ObtenerEmpleados([FromQuery] PaginacionDTO paginationDTO)
        {
            var queryable = _empleadoService.GetQueryable();
            await HttpContext.InsertarParametrosEnCabecera(queryable, paginationDTO.Paginas, paginationDTO.registrosPorPagina);
            return await _empleadoService.ObtenerEmpleados(paginationDTO);
        }


        [HttpGet("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<EmpleadoMuestraDTO>ObtenerEmpleadoPorId(int Id)
        {
             if(Id <= 0)
            {
                throw new CustomException ("Por favor ingrese un ID valido",409);
            
            }

            var empleadoMuestra = _empleadoService.ObtenerInformacionEmpleado(Id);

            return Ok(empleadoMuestra);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public ActionResult<EmpleadoMuestraDTO> ActualizarEmpleado([FromBody] EmpleadoCreacionDTO empleado, [FromQuery] int id)
        { 

            if(id <= 0)
            {
                throw new CustomException("Por favor ingrese un ID valido", 409);
            }

            _empleadoService.ObtenerInformacionEmpleado(id);

            return Ok(_empleadoService.ActualizarEmpleado(empleado,id));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<EmpleadoMuestraDTO> EliminarEmpleado(int id) 
        {

            if (id <= 0)
            {
                throw new CustomException("Por favor ingrese un ID valido", 409);
            }

             var empleadoAEliminar = _empleadoService.ObtenerInformacionEmpleado(id);
            _empleadoService.EliminarEmpleado(empleadoAEliminar);

             return Ok("El empleado fue eliminado con éxito.");
        }
    }
}
