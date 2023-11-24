namespace JS.Abp.AI;

public interface IChatProvider
{
    Task<string> CreateCompletion(string modelId ,List<ChatMessages> chatMessages,int maxTokens = 1024,bool returnAllResults = false, CancellationToken cancellationToken = default (CancellationToken)); 
}