using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Specifications;
using SharedKernel;
using System.Linq.Expressions;

namespace ReserveHub.Persistence.Specifications;

internal class GetAllSpacesSpec : Specification<Space>
{
    internal GetAllSpacesSpec(PaginationParams paginationParams)
    {
        AddIncludes(s => s.Reservations);
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
    private static Expression<Func<Space, object>> GetSortProperty(PaginationParams paginationQuery)
        => paginationQuery.SortColumn?.ToLower() switch
        {
            "name" => x => x.Name,
            "is_available" => x => x.IsActive,
            _ => x => x.Id,
        };
}
