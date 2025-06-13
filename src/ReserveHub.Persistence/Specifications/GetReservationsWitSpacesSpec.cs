using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Specifications;
using SharedKernel;
using System.Linq.Expressions;

namespace ReserveHub.Persistence.Specifications;

internal class GetReservationsWitSpacesSpec : Specification<Reservation>
{
    internal GetReservationsWitSpacesSpec(PaginationParams pagination)
    {
        AddPagination(pagination.Page, pagination.PageSize);
        AddIncludes(x => x.Space, x => x.User);
        if (!string.IsNullOrWhiteSpace(pagination.Q))
        {
            AddSearch(x => x.Space.Name.Contains(pagination.Q));
        }
        Expression<Func<Reservation, object>> keySelector = GetSortProperty(pagination);
        if (pagination.SortOrder is SortOrder.Desc)
        {
            AddOrderByDescending(keySelector);
        }
        else
        {
            AddOrderBy(keySelector);
        }
    }
    private static Expression<Func<Reservation, object>> GetSortProperty(PaginationParams pagination)
        => pagination.SortColumn?.ToLower() switch
        {
            "start_date" => x => x.StartTime,
            "end_date" => x => x.EndTime,
            _ => x => x.Id,
        };
}
