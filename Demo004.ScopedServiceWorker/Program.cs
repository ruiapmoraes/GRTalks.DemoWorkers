using Demo004.ScopedServiceWorker;
using Demo004.ScopedServiceWorker.Interfaces;
using Demo004.ScopedServiceWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<ISimpleService, SimpleService>();
        services.AddScoped<ICalculadoraService, CalculadoraService>();
        //services.AddHostedService<Worker>();
        services.AddHostedService<CalculadoraWorker>();
    })
    .Build();

host.Run();
