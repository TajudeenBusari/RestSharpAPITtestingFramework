using System.Net;
using FluentAssertions;
using RestSharp;
using Xunit.Abstractions;

namespace RestSharpAPITest;

public class ComponentsControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ComponentsControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task Test1()
    {
        //Disable SSL and pass other variables
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        //RestClient
        var client = new RestClient(restClientOptions);
        //RestRequest
        var request = new RestRequest("/Components/GetComponentByProductId/{id}");
        request.AddUrlSegment("id", 2);
        //perform Get
        var response = await client.GetAsync(request);
        //Assert
        
        response.Should().NotBeNull();

    }
}