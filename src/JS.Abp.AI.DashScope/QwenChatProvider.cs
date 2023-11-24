using JS.Abp.RestSharp;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace JS.Abp.AI.DashScope;

//更详细说明请参考https://help.aliyun.com/zh/dashscope/developer-reference/api-details?disableWebsiteRedirect=true
public class QwenChatProvider:ChatProviderBase,ITransientDependency
{
    protected IDashScopeSettingConfiguration DashScopeConfiguration { get; }
    private readonly IRestSharpManager _restSharpManager;
    public IJsonSerializer JsonSerializer { get; }
    public QwenChatProvider(IDashScopeSettingConfiguration dashScopeConfiguration,
        IRestSharpManager restSharpManager,
        IJsonSerializer jsonSerializer)
    {
        DashScopeConfiguration = dashScopeConfiguration;
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
        var apiKey = await DashScopeConfiguration.GetApiKeyAsync();
        QwenChatInput chatMessageInput =new QwenChatInput()
        {
            Messages = chatMessages.ToArray()
        };
        
        QwenChatMessage chatMessage = new QwenChatMessage()
        {
            Model = modelId,
            max_tokens = maxTokens,
            Input = chatMessageInput,
            Parameters = new QwenChatParameters()
            {
                Seed = 1,
                result_format = "message"
            }
        };
        var result = await _restSharpManager.ExecuteAsync("https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation",
            "Post", JsonSerializer.Serialize(chatMessage),"Json",new Dictionary<string, string>(
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
            var response = JsonSerializer.Deserialize<QwenChatResult>(result.Content!);
            return response.output.choices[0].message.Content;
        }
        else
        {
            throw new UserFriendlyException(result.ErrorMessage??"Unknown error!!!");
        }
    }
}