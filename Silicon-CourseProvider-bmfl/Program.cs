using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Contexts;
using Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.Mutations;
using Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.ObjectTypes;
using Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.Queries;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddPooledDbContextFactory<DataContext>(x =>
        {
            x.UseCosmos(Environment.GetEnvironmentVariable("COSMOS_URI")!, Environment.GetEnvironmentVariable("COSMOS_DBNAME")!)
            .UseLazyLoadingProxies();
        });

        services.AddScoped<ICourseService, CourseService>();

        services.AddGraphQLFunction()
            .AddQueryType<CourseQuery>()
            .AddType<CourseType>()
            .AddMutationType<CourseMutation>();

        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.EnsureCreated();

    })
    .Build();

host.Run();
