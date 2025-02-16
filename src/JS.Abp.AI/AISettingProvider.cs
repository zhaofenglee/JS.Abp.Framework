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
                AISettingNames.OpenAI.BaseDomain,
                "https://api.openai.com/",
                L("DisplayName:AISettingNames.OpenAI.BaseDomain"),
                L("Description:AISettingNames.OpenAI.BaseDomain"),true),
            
            new SettingDefinition(
                AISettingNames.OpenAI.ApiKey,
                "sk-",
                L("DisplayName:AISettingNames.OpenAI.ApiKey"),
                L("Description:AISettingNames.OpenAI.ApiKey"),false),
            
            new SettingDefinition(AISettingNames.OpenAI.Proxy,
                "",
                L("DisplayName:AISettingNames.OpenAI.Proxy"),
                L("Description:AISettingNames.OpenAI.Proxy"),true),
            
            new SettingDefinition(AISettingNames.OpenAI.Model,
                "gpt-3.5-turbo-16k,gpt-3.5-turbo-0301,gpt-3.5-turbo",
                L("DisplayName:AISettingNames.OpenAI.Model"),
                L("Description:AISettingNames.OpenAI.Model"),true),
            
            new SettingDefinition(AISettingNames.DashScope.ApiKey,
                "sk-",
                L("DisplayName:AISettingNames.DashScope.ApiKey"),
                L("Description:AISettingNames.DashScope.ApiKey"),false),
            
            new SettingDefinition(AISettingNames.DashScope.Model,
                "qwen-max",
                L("DisplayName:AISettingNames.DashScope.Model"),
                L("Description:AISettingNames.DashScope.Model"),true),

        new SettingDefinition(AISettingNames.AzureAI.Endpoint,
                "https://models.inference.ai.azure.com",
                L("DisplayName:AISettingNames.AzureAI.Endpoint"),
                L("Description:AISettingNames.AzureAI.Endpoint"),true),

        new SettingDefinition(AISettingNames.AzureAI.ApiKey,
                "github_pat",
                L("DisplayName:AISettingNames.AzureAI.ApiKey"),
                L("Description:AISettingNames.AzureAI.ApiKey"),false),

        new SettingDefinition(AISettingNames.AzureAI.Model,
                "gpt-4o-mini",
                L("DisplayName:AISettingNames.AzureAI.Model"),
                L("Description:AISettingNames.AzureAI.Model"),true),
            
        new SettingDefinition(AISettingNames.DeepSeek.Endpoint,
            "https://api.deepseek.com",
            L("DisplayName:AISettingNames.DeepSeek.Endpoint"),
            L("Description:AISettingNames.DeepSeek.Endpoint"),true),

        new SettingDefinition(AISettingNames.DeepSeek.ApiKey,
            "api_key",
            L("DisplayName:AISettingNames.DeepSeek.ApiKey"),
            L("Description:AISettingNames.DeepSeek.ApiKey"),false),

        new SettingDefinition(AISettingNames.DeepSeek.Model,
            "deepseek-chat",
            L("DisplayName:AISettingNames.DeepSeek.Model"),
            L("Description:AISettingNames.DeepSeek.Model"),true)
            
                );
    }
    
    
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AIResource>(name);
    }
}