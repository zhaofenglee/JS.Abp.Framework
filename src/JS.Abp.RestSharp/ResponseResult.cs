using System.Net;

namespace JS.Abp.RestSharp;

public class ResponseResult
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Content { get; set; }
    public string? ErrorMessage { get; set; }
}