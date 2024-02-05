using System;
using System.Net.Http;

namespace UAM.Core.Api;

public class ApiBase
{
    private readonly string _baseUrl;
    //private string BaseUrl => "http://5.188.141.251/";

    public ApiBase(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    protected HttpClient HttpClient => new HttpClient() { BaseAddress = new Uri(_baseUrl) };
}