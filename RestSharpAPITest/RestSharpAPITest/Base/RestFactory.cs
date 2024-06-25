namespace RestSharpAPITest.Base;

//interface
public interface IRestFactory
{
    IRestBuilder Create();
}

//concrete class
public class RestFactory : IRestFactory
{
    private readonly IRestBuilder _restBuilder;
    public RestFactory(IRestBuilder restBuilder)
    {
        _restBuilder = restBuilder;
    }

    public IRestBuilder Create()
    {
        return _restBuilder;
    }
}