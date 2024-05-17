using MassTransit;
using MassTransit.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using IMediator = MassTransit.Mediator.IMediator;

namespace MediatorsBenchmarkNet8;

public class MediatorsService
{
    private static readonly IRequestClient<MassTransitRequest> _massTransitMediator;
    private static readonly ISender _mediatRMediator;
    private static readonly ICustomMediator _customMediator;
    private static readonly INonGenericCustomMediator _nonGenericCustomMediator;

    static MediatorsService()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<MassTransitConsumer>());

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddTransient<ICustomRequestHandler<PingCustomRequest, string>, PingCustomHandler>();
        services.AddSingleton<ICustomMediator, CustomMediator>();

        services.AddTransient<INonGenericCustomPingRequestHandler, NonGenericCustomPingHandler>();
        services.AddSingleton<INonGenericCustomMediator, NonGenericCustomMediator>();

        var serviceProvider = services.BuildServiceProvider();

        var massTransitMediator = serviceProvider.GetRequiredService<IMediator>();
        _massTransitMediator = massTransitMediator.CreateRequestClient<MassTransitRequest>();

        _mediatRMediator = serviceProvider.GetRequiredService<ISender>();

        _customMediator = serviceProvider.GetRequiredService<ICustomMediator>();

        _nonGenericCustomMediator = serviceProvider.GetRequiredService<INonGenericCustomMediator>();
    }

    public static MassTransitResponse MassTransitMediator(MassTransitRequest request)
    {
        return _massTransitMediator.GetResponse<MassTransitResponse>(request).Result.Message;
    }

    public static MediatRResponse MediatRMediator(MediatRRequest request)
    {
        return _mediatRMediator.Send(request).Result;
    }

    public static string CustomMediator(PingCustomRequest request)
    {
        return _customMediator.Send(request).Result;
    }

    public static string NonGenericCustomMediator(NonGenericCustomPingRequest request)
    {
        return _nonGenericCustomMediator.SendPingRequest(request).Result;
    }

    public static DirectResponse Direct(MediatorNet8Request mediatorNet8Request)
    {
        return new DirectResponse(mediatorNet8Request.Message);
    }

}

public sealed record MediatorNet8Request(string Message);

public sealed record DirectResponse(string Message);