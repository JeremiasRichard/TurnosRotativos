using Microsoft.AspNetCore.Mvc;
using TurnosRotativos.DTOs;
using TurnosRotativos.Models;

namespace TurnosRotativos.Services
{
    public interface IEmpleadoService
    { 
       public EmpleadoMuestraDTO AltaEmpleado(EmpleadoCreacionDTO empleado);
       public IQueryable<Empleado> GetQueryable();
       public  Task<ActionResult<List<EmpleadoMuestraDTO>>> ObtenerEmpleados(PaginacionDTO paginacionDTO);
       public  EmpleadoMuestraDTO ObtenerInformacionEmpleado(int idEmpleado);
       public EmpleadoMuestraDTO ActualizarEmpleado(EmpleadoCreacionDTO empleado,int id);
       public EmpleadoMuestraDTO EliminarEmpleado(EmpleadoMuestraDTO empleado);
    }
}
