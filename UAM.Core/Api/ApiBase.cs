using System.Net.Http;

namespace UAM.Core.Api;

public class ApiBase
{
    private string BaseUrl => "https://localhost:7144/";
    
    protected HttpClient HttpClient => new HttpClient(){BaseAddress = new Uri(BaseUrl)};
}