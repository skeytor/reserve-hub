using MediatR;
using SharedKernel.Results;

namespace ReserveHub.Application.Handlers;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
