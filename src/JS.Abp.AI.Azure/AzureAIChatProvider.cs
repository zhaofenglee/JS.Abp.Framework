using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace JS.Abp.AI.Azure;

public class AzureAIChatProvider:ChatProviderBase,ITransientDependency
{
    protected IAzureSettingConfiguration AzureSettingConfiguration { get; }
    public IJsonSerializer JsonSerializer { get; }

    public AzureAIChatProvider(IAzureSettingConfiguration azureSettingConfiguration,IJsonSerializer jsonSerializer)
    {
        AzureSettingConfiguration = azureSettingConfiguration;
        JsonSerializer = jsonSerializer;
    }


    public override async Task<string> CreateCompletion(string modelId, List<ChatMessages> chatMessages, int maxTokens = 1024, bool returnAllResults = false,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var endpoint = new Uri(await AzureSettingConfiguration.GetEndpointAsync());
        var credential = new AzureKeyCredential(await AzureSettingConfiguration.GetApiKeyAsync());
        AzureOpenAIClient azureClient = new(
            endpoint,
            credential);

        var client =  azureClient.GetChatClient(modelId);
        List<ChatMessage> chatMessage = new List<ChatMessage>();
        foreach (var message in chatMessages)
        {
            if (message.Role.ToLower() == "user")
            {
                chatMessage.Add(new UserChatMessage(message.Content));
            }
            else
            {
                chatMessage.Add(new SystemChatMessage(message.Content));
            }
        }

        var chatCompletionOptions = new ChatCompletionOptions ()
        {
            Temperature = 1,
            MaxOutputTokenCount = maxTokens
        };

        ChatCompletion completion  = await client.CompleteChatAsync(chatMessage, chatCompletionOptions,cancellationToken);
        return completion.Content[0].Text;
    }
}