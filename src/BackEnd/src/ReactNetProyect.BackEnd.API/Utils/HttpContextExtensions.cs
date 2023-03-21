using Microsoft.EntityFrameworkCore;

namespace ReactNetProyect.BackEnd.API.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPagerParamsInHeader<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double quantity = await queryable.CountAsync();
            httpContext.Response.Headers.Add("cantidadTotalRegistros", quantity.ToString());
        }
    }
}
