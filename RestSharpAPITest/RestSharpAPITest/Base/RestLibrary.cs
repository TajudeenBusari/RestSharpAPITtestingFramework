
using GraphQLProductApp;
using GraphQLProductApp.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;

namespace RestSharpAPITest.Base;

//interface
public interface IRestLibrary
{
    RestClient RestClient { get; }
}

//concrete class
public class RestLibrary : IRestLibrary
{
    public RestLibrary(WebApplicationFactory<Program> webApplicationFactory)
    {
        //Disable SSL and pass other variables
       var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
       
       //spawn our SUT 
        var client = webApplicationFactory.CreateDefaultClient();
        
        RestClient = new RestClient(client, restClientOptions);
    }
    //get the RestClient
    public RestClient RestClient { get; }
}

/*By introducing the web application factory,
 our test classes will run without the instance of the application*/