namespace JS.Abp.AI.OpenAI;

public interface IOpenAISettingConfiguration: IAISettingConfiguration
{
    Task<string?> GetBaseDomainAsync();
    Task<string?> GetProxyAsync();
    
}