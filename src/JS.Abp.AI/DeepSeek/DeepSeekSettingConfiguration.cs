using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace JS.Abp.AI.DeepSeek;

public class DeepSeekSettingConfiguration:AISettingConfiguration,IDeepSeekSettingConfiguration,ITransientDependency
{
    public DeepSeekSettingConfiguration(ISettingProvider settingProvider) : base(settingProvider)
    {
    }

    public override Task<string?> GetApiKeyAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.DeepSeek.ApiKey);
    }
    
    public override Task<string?> GetModelAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.DeepSeek.Model);
    }
    
    public virtual Task<string?> GetEndpointAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.DeepSeek.Endpoint);
    }
}