using Demo006.DatabaseWorker;
using Demo006.DatabaseWorker.Models;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var conString = Utils.GetConnectionStrings("MyDatabase");

        services.AddDbContext<GrtalkDbContext>(options =>
             options.UseNpgsql(conString));

        //services.AddDbContext<GrtalkDbContext>(options =>
        //      options.UseNpgsql(conString
        //      , b => b.MigrationsAssembly(typeof(GrtalkDbContext).Assembly.FullName))
        //      .EnableSensitiveDataLogging(true));

        services.AddHostedService<DatabaseWorker>();
    })
    .Build();

host.Run();
