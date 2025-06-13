using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Specifications;
using SharedKernel;
using System.Linq.Expressions;

namespace ReserveHub.Persistence.Specifications;

internal class GetAvailableSpacesSpec : Specification<Space>
{
    internal GetAvailableSpacesSpec(PaginationParams paginationParams, DateTime startDate, DateTime endDate)
        : base(x => !x.Reservations.Any(r => r.StartTime < endDate && r.EndTime > startDate))
    {
        AddPagination(paginationParams.Page, paginationParams.PageSize);
        if (!string.IsNullOrWhiteSpace(paginationParams.Q))
        {
            AddSearch(x => x.Name.Contains(paginationParams.Q));
        }
        Expression<Func<Space, object>> keySelector = GetSortProperty(paginationParams);
        if (paginationParams.SortOrder is SortOrder.Desc)
        {
            AddOrderByDescending(keySelector);
        }
        else
        {
            AddOrderBy(keySelector);
        }
    }
    private static Expression<Func<Space, object>> GetSortProperty(PaginationParams paginationParams)
        => paginationParams.SortColumn?.ToLower() switch
        {
            "name" => x => x.Name,
            "is_available" => x => x.IsAvailable,
            _ => x => x.Id,
        };
}
