using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosRotativos.DTOs;
using TurnosRotativos.Exceptions;
using TurnosRotativos.Helpers;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Models;
using TurnosRotativos.Validations;

namespace TurnosRotativos.Services
{
    public class EmpleadoServiceImp : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public EmpleadoServiceImp(IEmpleadoRepository empleado, IMapper mapper)
        {
            _empleadoRepository = empleado;
            _mapper = mapper;
        }

        public EmpleadoMuestraDTO AltaEmpleado(EmpleadoCreacionDTO empleadoDTO)
        {
            Validadaciones.ValidarEmpleado(empleadoDTO);
            if (_empleadoRepository.ObtenerPorEmail(empleadoDTO.Email) != null)
            {
                throw new CustomException("Ya existe un empleado con el email ingresado.", 409);
            }

            if (_empleadoRepository.ObtenerPorDni(empleadoDTO.NroDocumento) != null)
            {
                throw new CustomException("Ya existe un empleado con el documento ingresado.", 409);
            }

            var empleadoMap = _mapper.Map<Empleado>(empleadoDTO);
            
            empleadoMap.FechaCreacion = DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy");
            empleadoMap.Jornadas = new List<Jornada>();

            _empleadoRepository.Crear(empleadoMap);
            _empleadoRepository.Guardar();

            return _mapper.Map<EmpleadoMuestraDTO>(empleadoMap);
        }

        public async Task<ActionResult<List<EmpleadoMuestraDTO>>> ObtenerEmpleados(PaginacionDTO paginacionDTO)
        {
            var empleados = await _empleadoRepository.ObtenerTodos().ToPaginate(paginacionDTO).ToListAsync();

            return _mapper.Map<List<EmpleadoMuestraDTO>>(empleados);
        }

        public  EmpleadoMuestraDTO ObtenerInformacionEmpleado(int id)
        {
            var empleado =  _empleadoRepository.ObtenerPorId(id);

            if(empleado != null)
            {
                return _mapper.Map<EmpleadoMuestraDTO>(empleado);
            }

            throw new CustomException("No se encontró el empleado con Id: "+ id, 404);
        }

        public IQueryable<Empleado> GetQueryable()
        {
            return _empleadoRepository.ObtenerTodos();
        }

        public EmpleadoMuestraDTO EliminarEmpleado(EmpleadoMuestraDTO empleado)
        {
            var empleadoEliminado = _mapper.Map<Empleado>(empleado);
            _empleadoRepository.Borrar(empleadoEliminado);

            return _mapper.Map<EmpleadoMuestraDTO>(empleadoEliminado);
        }

        public EmpleadoMuestraDTO ActualizarEmpleado (EmpleadoCreacionDTO empleadoConDatosNuevos ,int id)
        {
            Validadaciones.ValidarEmpleado(empleadoConDatosNuevos);
            var empleadoParaActualizar = _empleadoRepository.ObtenerPorId(id);
            ActualizarCampos(empleadoParaActualizar, empleadoConDatosNuevos);
            _empleadoRepository.Actualizar(empleadoParaActualizar);
            return _mapper.Map<EmpleadoMuestraDTO>(empleadoParaActualizar);
        }

  

        public void ActualizarCampos(Empleado empleadoParaActualizar, EmpleadoCreacionDTO empleado)
        {
            empleadoParaActualizar.Nombre = empleado.Nombre;
            empleadoParaActualizar.Apellido = empleado.Apellido;
            empleadoParaActualizar.NroDocumento = empleado.NroDocumento;
            empleadoParaActualizar.FechaNacimiento = empleado.FechaNacimiento;
            empleadoParaActualizar.FechaIngreso = empleado.FechaIngreso;
            empleadoParaActualizar.FechaCreacion = empleadoParaActualizar.FechaCreacion;
        }
    } 
}
