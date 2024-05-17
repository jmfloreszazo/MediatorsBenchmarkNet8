namespace MediatorsBenchmarkNet8;

public interface INonGenericCustomPingRequestHandler
{
    Task<string> Handle(NonGenericCustomPingRequest request);
}

public interface INonGenericCustomPingRequest
{
}

public class NonGenericCustomPingRequest : INonGenericCustomPingRequest
{
    public string Message { get; set; }

    public NonGenericCustomPingRequest(string message)
    {
        Message = message;
    }
}

public class NonGenericCustomPingHandler : INonGenericCustomPingRequestHandler
{
    public Task<string> Handle(NonGenericCustomPingRequest request)
    {
        return Task.FromResult(request.Message);
    }
}