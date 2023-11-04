using TurnosRotativos.DTOs;

namespace TurnosRotativos.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ToPaginate<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            return queryable
                .Skip((paginacionDTO.Paginas - 1) * paginacionDTO.registrosPorPagina)
                .Take(paginacionDTO.registrosPorPagina);
        }
    }
}