namespace MediatorsBenchmarkNet8;

public class CustomMediator : ICustomMediator
{
    private readonly IServiceProvider _serviceProvider;

    public CustomMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(ICustomRequest<TResponse> customRequest)
    {
        var requestType = customRequest.GetType();
        var handlerType = typeof(ICustomRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);
        var method = handlerType.GetMethod("Handle");
        var response = await (Task<TResponse>)method.Invoke(handler, new object[] { customRequest });

        return response;
    }
}

public interface ICustomRequestHandler<TRequest, TResponse> where TRequest : ICustomRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request);
}

public interface ICustomMediator
{
    Task<TResponse> Send<TResponse>(ICustomRequest<TResponse> request);

}

public interface ICustomRequest<TResponse>
{
}
