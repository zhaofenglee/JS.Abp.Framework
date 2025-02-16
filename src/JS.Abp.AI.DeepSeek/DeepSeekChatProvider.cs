using JS.Abp.AI.DashScope;
using JS.Abp.RestSharp;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace JS.Abp.AI.DeepSeek;


public class DeepSeekChatProvider:ChatProviderBase,ITransientDependency
{
    protected IDeepSeekSettingConfiguration DeepSeekSettingConfiguration { get; }
    private readonly IRestSharpManager _restSharpManager;
    public IJsonSerializer JsonSerializer { get; }
    public DeepSeekChatProvider(IDeepSeekSettingConfiguration deepSeekSettingConfiguration,
        IRestSharpManager restSharpManager,
        IJsonSerializer jsonSerializer)
    {
        DeepSeekSettingConfiguration = deepSeekSettingConfiguration;
        _restSharpManager = restSharpManager;
        JsonSerializer = jsonSerializer;
    }
    /// <summary>
    /// Create a completion
    /// messages指用户与模型的对话历史。list中的每个元素形式为{"role":角色, "content": 内容}。角色当前可选值：system、user、assistant，其中，仅messages[0]中支持role为system，user和assistant需要交替出现。
    /// </summary>
    public override async Task<string> CreateCompletion(string modelId, List<ChatMessages> chatMessages, int maxTokens = 1024,bool returnAllResults = false,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var url = await DeepSeekSettingConfiguration.GetEndpointAsync();
        var apiKey = await DeepSeekSettingConfiguration.GetApiKeyAsync();
        DeepSeekChatMessage chatMessageInput =new DeepSeekChatMessage()
        {
            Model = modelId,
            MaxTokens = maxTokens,
            Messages = chatMessages.ToArray()
        };
        
        var result = await _restSharpManager.ExecuteAsync(url,
            "Post", JsonSerializer.Serialize(chatMessageInput),"Json",new Dictionary<string, string>(
            ){
                {"Content-Type","application/json"},
                {"Authorization",$"Bearer {apiKey}"},
            });
        if (result.IsSuccessful)
        {
            if (returnAllResults)
            {
                return result.Content!;
            }
            var response = JsonSerializer.Deserialize<DeepSeekChatResult>(result.Content!);
            return response.Choices[0].Message.Content;
        }
        else
        {
            throw new UserFriendlyException(result.ErrorMessage??"Unknown error!!!");
        }
    }
}