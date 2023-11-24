using JS.Abp.AI.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace JS.Abp.AI;

internal class AISettingProvider: SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                AISettingNames.OpenAI.ApiKey,
                "sk-",
                L("DisplayName:AISettingNames.OpenAI.ApiKey"),
                L("Description:AISettingNames.OpenAI.ApiKey")),
            
            new SettingDefinition(AISettingNames.OpenAI.Proxy,
                "",
                L("DisplayName:AISettingNames.OpenAI.Proxy"),
                L("Description:AISettingNames.OpenAI.Proxy")),
            
            new SettingDefinition(AISettingNames.OpenAI.Model,
                "gpt-3.5-turbo-16k,gpt-3.5-turbo-0301,gpt-3.5-turbo",
                L("DisplayName:AISettingNames.OpenAI.Model"),
                L("Description:AISettingNames.OpenAI.Model")),
            
            new SettingDefinition(AISettingNames.DashScope.ApiKey,
                "sk-",
                L("DisplayName:AISettingNames.DashScope.ApiKey"),
                L("Description:AISettingNames.DashScope.ApiKey")),
            
            new SettingDefinition(AISettingNames.DashScope.Model,
                "qwen-max",
                L("DisplayName:AISettingNames.DashScope.Model"),
                L("Description:AISettingNames.DashScope.Model"))
            
                );
    }
    
    
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AIResource>(name);
    }
}