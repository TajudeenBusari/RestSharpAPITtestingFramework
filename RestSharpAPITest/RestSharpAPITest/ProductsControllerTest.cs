using System.Net;
using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpAPITest.Base;


namespace RestSharpAPITest;

public class ProductsControllerTest//:IClassFixture<RestLibrary> because we have alredy created an interface for our custom RestLibrary class
{
    
    private readonly IRestFactory _restFactory;
    private readonly RestClient _restClient;
    private readonly string? _token;
    public ProductsControllerTest(IRestFactory restFactory)
    
    {
       /* //Disable SSL and pass other variables
        _restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:44330"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };*/
       _restFactory = restFactory;
       _token = GetToken();
    }
    
    //Method to generate token
    private string GetToken()
    {
        var authResponse = _restFactory.Create()
            .WithRequest("/api/Authenticate/Login")
            .WithBody(new LoginModel
            {
                UserName = "KK",
                Password = "123456"
            })
            .WithPost().Result.Content;
        //Rest Client
        //var client = new RestClient(_restClientOptions);
        
        //Rest Request
        /*var authRequest = new RestRequest("/api/Authenticate/Login");
        
        //we can use the Login model as well
        //Typed object being used
        authRequest.AddJsonBody(new LoginModel()
        {
            UserName = "KK",
            Password = "123456"
        });
        //perform action method
        //Token is generated in form of string here and stored in response
        var authResponse = _restClient.PostAsync(authRequest).Result.Content;*/
        //parse token string from response
        var getToken = JObject.Parse(authResponse)["token"].ToString();
        return getToken;
    }
    
    //Get products by id
    [Fact]
    public async Task testGetProductById_ReturnsSuccess()
    {
       var productResponse = await _restFactory.Create()
            .WithRequest("/Product/GetProductById/1")
            .WithHeader("Authorization", $"Bearer {_token}")
            .WithGet<Product>();
       /* var productRequest = new RestRequest("/Product/GetProductById/1");
        productRequest.AddHeader("Authorization", $"Bearer {GetToken()}" ?? throw new InvalidOperationException());
        
        //perform action method
        var productResponse = await _restClient.GetAsync<Product>(productRequest);*/
       //Assert
        productResponse?.Name.Should().Be("Keyboard");
    }
    
    //Get products by query param
    [Fact]
    public async Task GetProductsByQueryParams()
    {
        //RestClient
        //var client = new RestClient(_restClientOptions);
        
        //RestRequest
        var response = await _restFactory.Create()
            .WithRequest("/Product/GetProductByIdAndName")
            .WithHeader("Authorization", $"Bearer {_token}")
            .WithQueryParameter("id", "2")
            .WithQueryParameter("name", "Monitor")
            .WithGet<Product>();
        /*var request = new RestRequest("/Product/GetProductByIdAndName?");
        request.AddHeader("Authorization", $"Bearer {_token}" ?? throw new InvalidOperationException());
        request.AddQueryParameter("id", 2);
        request.AddQueryParameter("name", "Monitor");
        //perform ActionMethod
        var response = await _restClient.GetAsync(request);*/
        //Assert
        response?.Name.Should().Be("Monitor");
    }
    
    //Create products
    [Fact]
    public async Task PostProducts()
    {
        var response = await _restFactory.Create()
            .WithRequest("/Product/Create")
            .WithHeader("Authorization", $"Bearer {_token}")
            .WithBody(new Product
            {
                Name = "Cabinet",
                Description = "Gaming Cabinet",
                Price = 12,
                ProductType = ProductType.PERIPHARALS
            })
            .WithPost<Product>();
        /*var request = new RestRequest("/Product/Create");
        request.AddHeader("Authorization", $"Bearer {_token}" ?? throw new InvalidOperationException());
        request.AddJsonBody(new Product
        {
            Name = "Cabinet",
            Description = "Gaming Cabinet",
            Price = 12,
            ProductType = ProductType.PERIPHARALS
        });
        //perform ActionMethod
        var response = await _restClient.PostAsync(request);*/
        //Assert
        response.Should().NotBeNull();
        response.Price.Should().Be(12);
    }
    
    //Upload file
    [Fact]
    public async Task FileUploadTest()
    {
        //create a builder pattern for file upload
        /*var response = await _restFactory.Create()
            .WithRequest("/Product")
            .WithHeader("Authorization", $"Bearer {_token}")
            .WithFileUpload("myFile",
                @"C:\Users\tajud\OneDrive\Desktop\someFolder\istockphoto-1320231994-2048x2048.jpg.jpg",
                "multipart/form-data")
            .WithPost<Product>();*/
        
        //using Rest Request
        //var request = new RestRequest("/Product", Method.Post);
        //request.AddHeader("Authorization", $"Bearer {_token}" ?? throw new InvalidOperationException());
        //request.AddFile("myFile", @"C:\Users\tajud\OneDrive\Desktop\someFolder\istockphoto-1320231994-2048x2048.jpg.jpg","multipart/form-data");
       
        //perform Execute
       //var response = await _restClient.ExecuteAsync<Product>(request);

        //Assert
       // response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
}