using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace JS.Abp.AI.Azure;

public class AzureSettingConfiguration:AISettingConfiguration,IAzureSettingConfiguration,ITransientDependency
{
    public AzureSettingConfiguration(ISettingProvider settingProvider) : base(settingProvider)
    {
    }
    
    public override Task<string?> GetApiKeyAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.AzureAI.ApiKey);
    }
    
    public override Task<string?> GetModelAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.AzureAI.Model);
    }

    public virtual Task<string?> GetEndpointAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.AzureAI.Endpoint);
    }
}