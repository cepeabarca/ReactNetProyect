using ReactNetProyect.Shared.DTO;

namespace ReactNetProyect.BackEnd.API.Utils
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Pager<T>(this IQueryable<T> queryable, PagerDTO pagerDTO)
        {
            return queryable
                .Skip((pagerDTO.Page - 1) * pagerDTO.RecordsXPage)
                .Take(pagerDTO.RecordsXPage);
        }
    }
}
