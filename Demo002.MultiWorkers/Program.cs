using Demo002.MultiWorkers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<AnotherWorker>();
    })
    .Build();

host.Run();
