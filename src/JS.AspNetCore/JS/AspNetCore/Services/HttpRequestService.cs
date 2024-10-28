using JS.Abp.Http.Services;
using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace JS.AspNetCore.Services;

public class HttpRequestService:IHttpRequestService, ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpRequestService(
        IJsonSerializer jsonSerializer,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        JsonSerializer = jsonSerializer;
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }
    /// <summary>
    /// 发起GET异步请求，返回HttpResponseMessage
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回HttpResponseMessage</returns>
    public async Task<HttpResponseMessage> GetHttpResponseMessageAsync(string url, Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        var hostName = GetHostName(url);
        using (HttpClient client = CreateHttpClient(hostName, headers, timeOut))
        {
            return await client.GetAsync(url);
        }
    }

    /// <summary>
    /// 发起GET异步请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回string</returns>
    public async Task<string> GetAsync(string url, Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        using (HttpResponseMessage response = await GetHttpResponseMessageAsync(url,headers,timeOut))
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else
            {
                return string.Empty;
            }
        }
    }
    
    /// <summary>
    /// 发起GET异步请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回T</returns>
    public async Task<T> GetAsync<T>(string url, Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        string responseString = await GetAsync(url, headers, timeOut);
        if (!string.IsNullOrWhiteSpace(responseString))
        {
            return JsonSerializer.Deserialize<T>(responseString);
        }
        else
        {
            return default(T);
        }
    }

    /// <summary>
    /// 发起POST异步请求，返回HttpResponseMessage
    /// </summary>
    public async Task<HttpResponseMessage> PostHttpResponseMessageAsync(string url, string? body, string bodyMediaType = "application/json", Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        var hostName = GetHostName(url);
        using (HttpClient client = CreateHttpClient(hostName, headers, timeOut))
        {
            HttpContent? content = body != null ? new StringContent(body, System.Text.Encoding.UTF8, bodyMediaType) : null;
            return await client.PostAsync(url, content);
        }
    }

    /// <summary>
    /// 发起POST异步请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="body">POST提交的内容</param>
    /// <param name="bodyMediaType">POST内容的媒体类型，如：application/xml、application/json</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回string</returns>
    public async Task<string> PostAsync(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30)
    {
        using (HttpResponseMessage response = await PostHttpResponseMessageAsync(url, body, bodyMediaType, headers, timeOut))
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 发起POST异步请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="body">POST提交的内容</param>
    /// <param name="bodyMediaType">POST内容的媒体类型，如：application/xml、application/json</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回T</returns>
    public async Task<T> PostAsync<T>(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30)
    {
        string responseString = await PostAsync(url, body, bodyMediaType, headers, timeOut);
        if (!string.IsNullOrWhiteSpace(responseString))
        {
            return JsonSerializer.Deserialize<T>(responseString);
        }
        else
        {
            return default(T);
        }
    }

    /// <summary>
    /// 发起PUT异步请求，返回HttpResponseMessage
    /// </summary>
    public async Task<HttpResponseMessage> PutHttpResponseMessageAsync(string url, string? body, string bodyMediaType = "application/json", Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        var hostName = GetHostName(url);
        using (HttpClient client = CreateHttpClient(hostName, headers, timeOut))
        {
            HttpContent? content = body != null ? new StringContent(body, System.Text.Encoding.UTF8, bodyMediaType) : null;
            return await client.PutAsync(url, content);
        }
    }


    /// <summary>
    /// 发起PUT异步请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="body">POST提交的内容</param>
    /// <param name="bodyMediaType">POST内容的媒体类型，如：application/xml、application/json</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回string</returns>
    public async Task<string> PutAsync(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30)
    {
        using (HttpResponseMessage response = await PutHttpResponseMessageAsync(url, body, bodyMediaType, headers, timeOut))
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 发起PUT异步请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="body">POST提交的内容</param>
    /// <param name="bodyMediaType">POST内容的媒体类型，如：application/xml、application/json</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回T</returns>
    public async Task<T> PutAsync<T>(string url, string? body,
        string bodyMediaType = "application/json",
        Dictionary<string, string>? headers = null,
        int timeOut = 30)
    {
        string responseString = await PutAsync(url, body, bodyMediaType, headers, timeOut);
        if (!string.IsNullOrWhiteSpace(responseString))
        {
            return JsonSerializer.Deserialize<T>(responseString);
        }
        else
        {
            return default(T);
        }
    }

    /// <summary>
    /// 发起DELETE异步请求，返回HttpResponseMessage
    /// </summary>
    public async Task<HttpResponseMessage> DeleteHttpResponseMessageAsync(string url, Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        var hostName = GetHostName(url);
        using (HttpClient client = CreateHttpClient(hostName, headers, timeOut))
        {
            return await client.DeleteAsync(url);
        }
    }

    /// <summary>
    /// 发起DELETE异步请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回string</returns>
    public async Task<string> DeleteAsync(string url, Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        using (HttpResponseMessage response = await DeleteHttpResponseMessageAsync(url, headers, timeOut))
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 发起DELETE异步请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头信息</param>
    /// <param name="timeOut">请求超时时间，单位秒</param>
    /// <returns>返回T</returns>
    public async Task<T> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, int timeOut = 30)
    {
        string responseString = await DeleteAsync(url, headers, timeOut);
        if (!string.IsNullOrWhiteSpace(responseString))
        {
            return JsonSerializer.Deserialize<T>(responseString);
        }
        else
        {
            return default(T);
        }
    }

    #region 私有函数

    /// <summary>
    /// 获取请求的主机名
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private static string GetHostName(string url)
    {
        if (!string.IsNullOrWhiteSpace(url))
        {
            return url.Replace("https://", "").Replace("http://", "").Split('/')[0];
        }
        else
        {
            return "AnyHost";
        }
    }

    private HttpClient CreateHttpClient(string hostName, Dictionary<string, string>? headers, int timeOut)
    {
        HttpClient client = _httpClientFactory.CreateClient(hostName);
        client.Timeout = TimeSpan.FromSeconds(timeOut);
        if (headers?.Count > 0)
        {
            foreach (string key in headers.Keys)
            {
                client.DefaultRequestHeaders.Add(key, headers[key]);
            }
        }
        var httpContext = _httpContextAccessor.HttpContext;
        var cookies = httpContext?.Request.Cookies;
        var token = cookies?["XSRF-TOKEN"];
        var cookie = (IEnumerable<string?>)httpContext.Request.Headers["Cookie"];
        // Authorization
        if (!client.DefaultRequestHeaders.Contains("Authorization") &&
            _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out var accessToken) == true)
        {
            client.DefaultRequestHeaders.Add("Authorization", (string?)accessToken ?? string.Empty);
        }
        else
        {
            client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        // Add XSRF-TOKEN if it exists
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Add("RequestVerificationToken", token);
        }
        
        return client;
    }

    #endregion

}