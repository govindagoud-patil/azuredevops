﻿using CleanArch.Core.Interfaces;
using CleanArch.Core.Services;
using CleanArch.Infrastructure.Data;
using CleanArch.Infrastructure.Data.Queries;
using CleanArch.UseCases.Contributors.List;


namespace CleanArch.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("SqliteConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlite(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           .AddScoped<IDeleteContributorService, DeleteContributorService>();


    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
