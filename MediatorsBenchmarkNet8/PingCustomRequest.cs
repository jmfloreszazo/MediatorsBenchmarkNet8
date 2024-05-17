namespace MediatorsBenchmarkNet8;

public class PingCustomRequest : ICustomRequest<string>
{
    public PingCustomRequest(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}
