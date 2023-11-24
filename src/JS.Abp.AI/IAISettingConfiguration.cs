namespace JS.Abp.AI;

public interface IAISettingConfiguration
{
    Task<string> GetApiKeyAsync();
    Task<string> GetModelAsync();
}