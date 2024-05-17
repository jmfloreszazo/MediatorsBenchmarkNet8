using Microsoft.Extensions.DependencyInjection;

namespace MediatorsBenchmarkNet8;

public class NonGenericCustomMediator : INonGenericCustomMediator
{
    private readonly IServiceProvider _serviceProvider;

    public NonGenericCustomMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<string> SendPingRequest(NonGenericCustomPingRequest request)
    {
        var handler = _serviceProvider.GetService<INonGenericCustomPingRequestHandler>();
        var response = await handler.Handle(request);
        return response;
    }
}

public interface INonGenericCustomMediator
{
    Task<string> SendPingRequest(NonGenericCustomPingRequest request);
}