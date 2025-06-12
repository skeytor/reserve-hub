using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Specifications;
using SharedKernel;
using System.Linq.Expressions;

namespace ReserveHub.Persistence.Specifications;

internal class GetAvailableSpacesSpec : Specification<Space>
{
    internal GetAvailableSpacesSpec(DateTime startDate, DateTime endDate, PaginationParams paginationParams)
        : base(s => !s.Reservations.Any(r => r.StartTime < endDate && r.EndTime > startDate))
        //: base(s => !s.Reservations.Any())
    {
        AddPagination(paginationParams.Page, paginationParams.PageSize);
        if (!string.IsNullOrWhiteSpace(paginationParams.SearchTerm))
        {
            AddSearch(x => x.Name.Contains(paginationParams.SearchTerm));
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
            "is_available" => x => x.IsActive,
            _ => x => x.Id,
        };
}
