using MediatR;

namespace MediatorsBenchmarkNet8;

public interface ICustomMediator
{
    Task<TResponse> Send<TResponse>(ICustomRequest<TResponse> request);

}