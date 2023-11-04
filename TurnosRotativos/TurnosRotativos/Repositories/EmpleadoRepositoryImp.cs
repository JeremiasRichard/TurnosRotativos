using Microsoft.EntityFrameworkCore;
using System.Net;
using TurnosRotativos.Database;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Models;

namespace TurnosRotativos.Repositories
{
    public class EmpleadoRepositoryImp : IEmpleadoRepository
    {

        private readonly ApplicationDbContext _context;

        public EmpleadoRepositoryImp(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Empleado Crear(Empleado entity)
        {
            _context.Add(entity);
            return entity;
        }
        public  IQueryable<Empleado> ObtenerTodos()
        {
            return _context.Empleados.AsQueryable();
        }
        public  Empleado ObtenerPorId(int id)
        {
            return _context.Empleados
                .Where(empleado => empleado.Id == id)
                .Include(empleado => empleado.Jornadas)
                .FirstOrDefault();
        }
        public Empleado ObtenerPorDni(int dni)
        {
            return _context.Empleados.Where(empleado => empleado.NroDocumento == dni).FirstOrDefault();
        }
        public Empleado ObtenerPorEmail(string email)
        {
            return _context.Empleados.Where(empleado => empleado.Email.Equals(email)).FirstOrDefault();
        }
        public Empleado Actualizar(Empleado entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public Empleado Borrar(Empleado entity)
        {
            _context.Empleados.Remove(entity);
            _context.SaveChanges();
            return entity;
        }
        public bool Guardar()
        {
            var saved = _context.SaveChanges();
            return saved > 0; 
        }
        public bool Existe(int dni)
        {
           return _context.Empleados.Any(x => x.NroDocumento == dni);
        }
    }
}
