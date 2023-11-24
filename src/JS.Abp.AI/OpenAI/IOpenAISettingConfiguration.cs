namespace JS.Abp.AI.OpenAI;

public interface IOpenAISettingConfiguration: IAISettingConfiguration
{
    Task<string> GetProxyAsync();
    
}