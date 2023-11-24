namespace JS.Abp.AI;

public abstract class ChatProviderBase:IChatProvider
{
    public abstract Task<string> CreateCompletion(string modelId, List<ChatMessages> chatMessages, int maxTokens=1024,bool returnAllResults = false,
        CancellationToken cancellationToken = default(CancellationToken));
}