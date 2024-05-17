namespace MediatorsBenchmarkNet8;

public class PingCustomHandler : ICustomRequestHandler<PingCustomRequest, string>
{
    public Task<string> Handle(PingCustomRequest customRequest)
    {
        return Task.FromResult(customRequest.Message);
    }
}

public class PingCustomRequest : ICustomRequest<string>
{
    public PingCustomRequest(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}