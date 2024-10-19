using Azure;
using Azure.AI.Inference;
using JS.Abp.AI.DashScope;
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

        var client = new ChatCompletionsClient(endpoint, credential, new ChatCompletionsClientOptions());
        List<ChatRequestMessage> chatMessage = new List<ChatRequestMessage>();
        foreach (var message in chatMessages)
        {
            if (message.Role.ToLower() == "user")
            {
                chatMessage.Add(new ChatRequestUserMessage(message.Content));
            }
            else
            {
                chatMessage.Add(new ChatRequestSystemMessage(message.Content));
            }
        }

        var requestOptions = new ChatCompletionsOptions(
            messages: chatMessage
        )
        {
            Model = modelId,
            Temperature = 1,
            MaxTokens = maxTokens
        };

        Response<ChatCompletions> response = await client.CompleteAsync(requestOptions);
        return response.Value.Choices[0].Message.Content;
    }
}