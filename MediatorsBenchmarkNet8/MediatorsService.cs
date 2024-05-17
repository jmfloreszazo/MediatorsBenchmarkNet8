using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using IMediator = MassTransit.Mediator.IMediator;

namespace MediatorsBenchmarkNet8;

public class MediatorsService
{
    private static readonly IRequestClient<MassTransitRequest> _massTransitMediator;
    private static readonly ISender _mediatRMediator;

    static MediatorsService()
    {
        IServiceCollection services = new ServiceCollection();

        // MassTransit
        services
            .AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<MassTransitConsumer>());

        var serviceProvider = services.BuildServiceProvider();

        var mediator = serviceProvider.GetRequiredService<IMediator>();
        _massTransitMediator = mediator.CreateRequestClient<MassTransitRequest>();

        // MediatR
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        serviceProvider = services.BuildServiceProvider();

        _mediatRMediator = serviceProvider.GetRequiredService<ISender>();
    }

    public static MassTransitResponse MassTransitMediator(MassTransitRequest request)
    {
        return _massTransitMediator.GetResponse<MassTransitResponse>(request).Result.Message;
    }

    public static MediatRResponse MediatRMediator(MediatRRequest request)
    {
        return _mediatRMediator.Send(request).Result;
    }

    public static DirectResponse Direct(MediatorNet8Request mediatorNet8Request)
    {
        return new DirectResponse(mediatorNet8Request.Message);
    }
}

public sealed record MediatorNet8Request(string Message);

public sealed record DirectResponse(string Message);