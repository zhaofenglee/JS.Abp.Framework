namespace JS.Abp.AI.Azure;

public interface IAzureSettingConfiguration: IAISettingConfiguration
{
    Task<string?> GetEndpointAsync();
}