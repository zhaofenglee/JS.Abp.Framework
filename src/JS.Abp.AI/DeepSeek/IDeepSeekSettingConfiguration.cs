namespace JS.Abp.AI.DeepSeek;

public interface IDeepSeekSettingConfiguration:IAISettingConfiguration
{
    Task<string?> GetEndpointAsync();
}