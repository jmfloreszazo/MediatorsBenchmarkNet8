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

        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for type {requestType.Name}");
        }

        var method = handlerType.GetMethod("Handle");
        var response = await (Task<TResponse>)method.Invoke(handler, new object[] { customRequest });

        return response;
    }

}

public sealed record class CustomMediatorResponse(string? Message);
