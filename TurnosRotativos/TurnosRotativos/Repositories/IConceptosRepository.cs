using TurnosRotativos.Models;

namespace TurnosRotativos.Interfaces
{
    public interface IConceptosRepository
    { 
        public IQueryable<Concepto> ObtenerConceptos();
        public Concepto ObtenerConceptoPorId(int id);
    }
}
