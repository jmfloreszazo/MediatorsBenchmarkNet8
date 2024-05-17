namespace MediatorsBenchmarkNet8;
public interface ICustomRequestHandler<TRequest, TResponse> where TRequest : ICustomRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request);
}