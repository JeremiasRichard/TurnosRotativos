using TurnosRotativos.Models;

namespace TurnosRotativos.Interfaces
{
    public interface IEmpleadoRepository
    {
        Empleado Crear(Empleado entity);
        Empleado Actualizar(Empleado entity);
        Empleado Borrar(Empleado entity);
        public Empleado ObtenerPorId(int id);
        public IQueryable<Empleado> ObtenerTodos();
        public bool Guardar();
        public bool Existe(int dni);
        public Empleado ObtenerPorDni(int dni);
        public Empleado ObtenerPorEmail(string email);

    }
}
