using RestSharp;
using Volo.Abp.DependencyInjection;

namespace JS.Abp.RestSharp;

public class RestSharpManager: IRestSharpManager,ITransientDependency
{
    public  async Task<ResponseResult> ExecuteAsync(string url,string method = "Get", string? body = null,string dataFormat = "Json", Dictionary<string, string>? headers = null, int maxTimeout = -1)
    {
        Uri uri = new Uri(url);
        string baseUrl = uri.GetLeftPart(UriPartial.Authority);
        string resource = uri.PathAndQuery;
        var options = new RestClientOptions(baseUrl)
        {
            Timeout = TimeSpan.FromMilliseconds(maxTimeout),
        };
        var client = new RestClient(options);
        var request = new RestRequest(resource,ConvertMethod(method));
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        if (body != null)
        {
            request.AddStringBody(body,ConvertDataFormat(dataFormat));
        }
        RestResponse response = await client.ExecuteAsync(request);
        return new ResponseResult
        {
            StatusCode = response.StatusCode,
            IsSuccessful = response.IsSuccessful,
            Content = response.Content,
            ErrorMessage = response.ErrorMessage
        };
    }
    
    
    /// <summary>
    /// ConvertMethod
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    private static Method ConvertMethod(string method)
    {
        return method switch
        {
            "Get" => Method.Get,
            "Post" => Method.Post,
            "Put" => Method.Put,
            "Delete" => Method.Delete,
            "Head" => Method.Head,
            "Options" => Method.Options,
            "Patch" => Method.Patch,
            _ => throw new ArgumentException("Invalid method: " + method, nameof(method))
        };
    }
    
    /// <summary>
    /// ConvertDataFormat
    /// </summary>
    /// <param name="dataFormat"></param>
    /// <returns></returns>
    private static DataFormat ConvertDataFormat(string dataFormat)
    {
        return dataFormat switch
        {
            "Json" => DataFormat.Json,
            "Xml" => DataFormat.Xml,
            "Binary" => DataFormat.Binary,
            "None" => DataFormat.None,
            _ => throw new ArgumentException("Invalid dataFormat: " + dataFormat, nameof(dataFormat))
        };
    }
}