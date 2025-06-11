using MediatR;
using SharedKernel.Results;

namespace ReserveHub.Application.Handlers;

public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
