using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace MediatorsBenchmarkNet8;

[MemoryDiagnoser]
public class Program
{

    //dotnet run -c Release -f net8.0

    private const string _message = "Hello World!";

    private static void Main()
    {
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);

        BenchmarkRunner.Run<Program>(config);
    }

    [Benchmark]
    public void MassTransit()
    {
        MediatorsService.MassTransitMediator(new MassTransitRequest(_message));
    }

    [Benchmark]
    public void MediatR()
    {
        MediatorsService.MediatRMediator(new MediatRRequest(_message));
    }

    [Benchmark]
    public void MediatorNet8()
    {
        MediatorsService.Direct(new MediatorNet8Request(_message));
    }
}