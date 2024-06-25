using System.Net.Http.Headers;
using System.Text;
using GraphQLProductApp;
using GraphQLProductApp.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RestSharpIntegrationTest;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;
    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    [Fact]
    public async Task Test1()
    {
        var application = new WebApplicationFactory<Program>();

        var client = application.CreateDefaultClient();

        var data = new
        {
            UserName = "KK",
            Password = "123456"
        };
        //post request for authorization
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var authResponse = await client.PostAsync("api/Authenticate/Login", content);
        var tokenAsString = authResponse.Content.ReadAsStringAsync();
        var token = JObject.Parse(await tokenAsString)["token"];
        //pass the token in the header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.ToString());
        
        //action method
        var response = await client.GetAsync($"Product/GetProductById/2");
        
        //assert and print to console
        response.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test2()
    {
        var application = new WebApplicationFactory<Program>();

        var client = application.CreateDefaultClient();
        
       /* var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };*/
        
        //Rest client
        var restClient = new RestClient(client);

        var authRequest = new RestRequest("api/Authenticate/Login");
        
        authRequest.AddJsonBody(new
        {
            UserName = "KK",
            Password = "123456"
        });

        var authResponse = restClient.PostAsync(authRequest).Result.Content;
        
        var token = JObject.Parse(authResponse)["token"];
        //pass the token in the header
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.ToString());
        restClient.AddDefaultHeader("Authorization", "Bearer " + token);
        
        //action method
        var response = await restClient.GetAsync<Product>(new RestRequest($"Product/GetProductById/2"));
        
        //assert and print to console
        _testOutputHelper.WriteLine(response?.Name);
        _testOutputHelper.WriteLine(response?.Price.ToString());
    }
}


/*Add the following nugget package:
 * Microsoft.AspNetCore.MVC.Testing
 * RestSharp
 * Fluent assertions
 *
 * FOR TEST 1
 * 1. Create a new instance of a web application factory from the MVC testing nugget package
 *2.Pass the starting point of the application i.e. Program
 *3. Add the project reference, right-click on the IntegrationTest Project(dependencies)
 * and add the GraphQL.Net as reference
 * 4.Create a client and follow other steps
 *
 *FOR TEST2
 * We will use the RestSharp dependency
 * 
 */