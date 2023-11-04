using TurnosRotativos.Database;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Models;

namespace TurnosRotativos.Repositories
{
    public class ConceptoRepositoryImp : IConceptosRepository
    {
        private readonly ApplicationDbContext _context;

        public ConceptoRepositoryImp(ApplicationDbContext context)
        {
            _context = context;
        }

        public Concepto ObtenerConceptoPorId(int id)
        {
           return  _context.Conceptos.Where(x => x.Id == id).FirstOrDefault();
        }
        public IQueryable<Concepto> ObtenerConceptos()
        {
          return  _context.Conceptos.AsQueryable();
        }
    }
}
