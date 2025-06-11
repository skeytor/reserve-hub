using MediatR;
using SharedKernel.Results;

namespace ReserveHub.Application.Handlers;

public interface IQuery : IRequest<Result>;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
