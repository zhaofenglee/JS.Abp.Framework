namespace JS.Abp.RestSharp;

public interface IRestSharpManager
{
    Task<ResponseResult> ExecuteAsync(string url,string method = "Get", string? body = null,string dataFormat = "Json", Dictionary<string, string>? headers = null, int maxTimeout = -1);
}