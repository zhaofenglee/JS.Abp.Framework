using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace JS.Abp.AI.OpenAI;

public class OpenAISettingConfiguration:AISettingConfiguration,IOpenAISettingConfiguration,ITransientDependency
{
    public OpenAISettingConfiguration(ISettingProvider settingProvider) : base(settingProvider)
    {
    }

    public override Task<string> GetApiKeyAsync()
    {
        return GetNotEmptySettingValueAsync(AISettingNames.OpenAI.ApiKey);
    }

    public Task<string> GetProxyAsync()
    { 
        return GetNotEmptySettingValueAsync(AISettingNames.OpenAI.Proxy);
    }

    public override Task<string> GetModelAsync()
    {
        return GetNotEmptySettingValueAsync(AISettingNames.OpenAI.Model);
    }
}