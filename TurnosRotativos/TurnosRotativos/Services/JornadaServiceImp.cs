using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TurnosRotativos.DTOs;
using TurnosRotativos.Exceptions;
using TurnosRotativos.Helpers;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Models;

namespace TurnosRotativos.Services
{
    public class JornadaServiceImp : IJornadaService
    {
        private readonly IJornadaRepository _jornadaRepository;
        private readonly IConceptosRepository _conceptosRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;
        public JornadaServiceImp(IJornadaRepository jornadaRepository, IConceptosRepository conceptosRepository, IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _jornadaRepository = jornadaRepository;
            _conceptosRepository = conceptosRepository;
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public JornadaCreacionMuestraDTO CrearJornada(JornadaCreacionDTO jornada)
        {
            var concepto = _conceptosRepository.ObtenerConceptoPorId(jornada.IdConcepto);
            var empleado = _empleadoRepository.ObtenerPorId(jornada.IdEmpleado);
           
            if(concepto == null)
            {
                throw new CustomException("No existe el concepto ingresado", 404);
            }

            if(empleado == null)
            {
                throw new CustomException("No existe empleado con el ID ingresado", 404);
            }

            var jornadaMap = _mapper.Map<Jornada>(jornada);

            ValidacionesJornada(empleado, jornadaMap);

            jornadaMap.HorasTrabajadas = jornada.HorasTrabajadas;

            var jornadaCreada = _jornadaRepository.CrearJornada(jornadaMap);
            _jornadaRepository.Guardar();
 
           return _mapper.Map<JornadaCreacionMuestraDTO>(jornadaCreada); 
        }

        public IQueryable<Jornada> GetQueryable()
        {
            return _jornadaRepository.ObtenerJornadas();
        }

        public async Task<ActionResult<List<JornadaMuestraDTO>>> ObtenerJornadasPorFechaODNI(PaginacionDTO paginacionDTO, string fecha, int?dni)
        {
            if (!fecha.IsNullOrEmpty() && dni == null)
            {
                var jornadas = await _jornadaRepository.ObtenerJornadas().Where(x => x.Fecha.Equals(fecha)).ToPaginate(paginacionDTO).ToListAsync();
                return _mapper.Map<List<JornadaMuestraDTO>>(jornadas);
            }

            if (fecha.IsNullOrEmpty() && dni != null)
            {
                var jornadas = await _jornadaRepository.ObtenerJornadas().Where(x => x.Empleado.NroDocumento == dni).ToPaginate(paginacionDTO).ToListAsync();
                return _mapper.Map<List<JornadaMuestraDTO>>(jornadas);
            }
            else
            {
                var jornadas = await _jornadaRepository.ObtenerJornadas().ToPaginate(paginacionDTO).ToListAsync();
                return _mapper.Map<List<JornadaMuestraDTO>>(jornadas);
            }
        }

        // --  Validaciones para la creacion de la jornada -- // 
        public static void ValidarInsercionNuevaJornada(Empleado empleado, Jornada nueva)
        {   
            foreach (var jornada in empleado.Jornadas)
            {
                if (jornada.IdConcepto == nueva.IdConcepto && jornada.Fecha.Equals(nueva.Fecha))
                {
                    throw new CustomException("El empleado ya tiene registrada una jornada con este concepto en la fecha ingresada.", 400);
                }

                if (jornada.IdConcepto == 3 && jornada.Fecha.Equals(nueva.Fecha))
                {
                    throw new CustomException("El empleado ingresado cuenta con un día libre en esa fecha", 400);
                }

                if (nueva.IdConcepto == 3 && jornada.Fecha.Equals(nueva.Fecha))
                {
                    throw new CustomException("El empleado no puede cargar Dia Libre si cargo un turno previamente para la fecha: " + nueva.Fecha, 400);
                }
            }
        }

        public static void ValidarHorasDeJornada(Empleado empleado, Jornada nueva)
        {
            foreach (var jornada in empleado.Jornadas)
            {
                if (jornada.Fecha.Equals(nueva.Fecha) && (jornada.HorasTrabajadas + nueva.HorasTrabajadas) > 12)
                {
                    throw new CustomException("El empleado no puede cargar mas de 12 horas trabajas en un dia", 400);
                }
            }
        }

        public static void ValidarCreacionDeJornada(Jornada nueva)
        {
            if (nueva.IdConcepto == 1 && (nueva.HorasTrabajadas > 8 || nueva.HorasTrabajadas < 6))
            {
                throw new CustomException("El rango de horas que se puede cargar para este concepto es de: Minimo = 6 y Maximo = 8 ", 400);
            }
            else if (nueva.IdConcepto == 2 && (nueva.HorasTrabajadas > 6 || nueva.HorasTrabajadas < 2))
            {
                throw new CustomException("El rango de horas que se puede cargar para este concepto es de: Minimo = 2 y Maximo = 6", 400);
            }
            else if (nueva.IdConcepto == 3 && (nueva.HorasTrabajadas != null || nueva.HorasTrabajadas != null))
            {
                throw new CustomException("El rango de horas que se puede cargar para este concepto es de: Minimo = 0 y Maximo = 0 ", 400);
            }
        }

        public static void ValidarHorasSemanales(Empleado empleado, Jornada nueva)
        {
            DateTime fechaJornadaNueva = DateTime.ParseExact(nueva.Fecha, "dd/MM/yyyy", null);
            // Resta 7 días a la fecha de la jornada nueva para hacer la comparacion
            DateTime fechaDeFiltro = fechaJornadaNueva.AddDays(-7);

            int ? hsTrabajadas = 0;

            foreach (var jornada in empleado.Jornadas)
            {   
                DateTime fechaJornada = DateTime.ParseExact(jornada.Fecha, "dd/MM/yyyy", null);

                if (fechaJornada > fechaDeFiltro)
                {  
                    if(jornada.HorasTrabajadas  !=null)
                    {
                        hsTrabajadas += jornada.HorasTrabajadas;
                    }
                }
            }

            if (hsTrabajadas  > 48)
            {
                throw new CustomException("El empleado ingresado supera las 48hs semanales", 400);
            }
            else if(hsTrabajadas + nueva.HorasTrabajadas > 48)
            {
                throw new CustomException("El empleado ingresado superará las 48hs semanales", 400);
            }
        }

        public static void ValidacionesJornada(Empleado empleado, Jornada nueva)
        {
            ValidarInsercionNuevaJornada(empleado, nueva);
            ValidarHorasSemanales(empleado,nueva);
            ValidarHorasDeJornada(empleado, nueva);
            ValidarCreacionDeJornada(nueva);
        }
    }
}
