namespace MediatorsBenchmarkNet8;

public class PingCustomHandler : ICustomRequestHandler<PingCustomRequest, string>
{
    public Task<string> Handle(PingCustomRequest customRequest)
    {
        return Task.FromResult(customRequest.Message);
    }
}