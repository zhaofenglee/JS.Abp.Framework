namespace JS.Abp.Http.Services;

public interface IHttpRequestService
{
    Task<HttpResponseMessage> GetHttpResponseMessageAsync(string url, Dictionary<string, string>? headers = null,
        int timeOut = 30);

    Task<string> GetAsync(string url, Dictionary<string, string>? headers = null, int timeOut = 30);

    Task<T> GetAsync<T>(string url, Dictionary<string, string>? headers = null, int timeOut = 30);

    Task<HttpResponseMessage> PostHttpResponseMessageAsync(string url, string? body,
        string bodyMediaType = "application/json", Dictionary<string, string>? headers = null, int timeOut = 30);

    Task<string> PostAsync(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30);

    Task<T> PostAsync<T>(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30);

    Task<HttpResponseMessage> PutHttpResponseMessageAsync(string url, string? body,
        string bodyMediaType = "application/json", Dictionary<string, string>? headers = null, int timeOut = 30);

    Task<string> PutAsync(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30);

    Task<T> PutAsync<T>(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30);

    Task<HttpResponseMessage> DeleteHttpResponseMessageAsync(string url, Dictionary<string, string>? headers = null,
        int timeOut = 30);

    Task<string> DeleteAsync(string url, Dictionary<string, string>? headers = null, int timeOut = 30);

    Task<T> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, int timeOut = 30);

}