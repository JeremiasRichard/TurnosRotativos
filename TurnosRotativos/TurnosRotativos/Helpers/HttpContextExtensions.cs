using Microsoft.EntityFrameworkCore;

namespace TurnosRotativos.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosEnCabecera<T>(this HttpContext httpContext,
          IQueryable<T> queryable, int page, int recordsPerPage)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            double quantity = await queryable.CountAsync();
            httpContext.Response.Headers.Add("TotalRegisters", quantity.ToString());
            httpContext.Response.Headers.Add("RecordsPerPage", recordsPerPage.ToString());
            httpContext.Response.Headers.Add("Page", page.ToString());
        }
    }
}
