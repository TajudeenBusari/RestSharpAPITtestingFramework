
using GraphQLProductApp;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RestSharpAPITest.Base;

namespace RestSharpAPITest;

public class Startup
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IRestLibrary>(new RestLibrary( new WebApplicationFactory<Program>()))
            .AddScoped<IRestBuilder, RestBuilder>()
            .AddScoped<IRestFactory, RestFactory>();
    }
}
//start up class to configure/register the dependency injection