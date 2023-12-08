using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace JS.Abp.AI.OpenAI;

public class OpenAISettingConfiguration:AISettingConfiguration,IOpenAISettingConfiguration,ITransientDependency
{
    public OpenAISettingConfiguration(ISettingProvider settingProvider) : base(settingProvider)
    {
    }
    public Task<string?> GetBaseDomainAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.OpenAI.BaseDomain);
    }

    public override Task<string?> GetApiKeyAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.OpenAI.ApiKey);
    }

    public Task<string?> GetProxyAsync()
    { 
        return GetOrNullSettingValueAsync(AISettingNames.OpenAI.Proxy);
    }

    public override Task<string?> GetModelAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.OpenAI.Model);
    }
}