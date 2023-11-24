using Volo.Abp;
using Volo.Abp.Settings;

namespace JS.Abp.AI;

public abstract class AISettingConfiguration:IAISettingConfiguration
{
    protected ISettingProvider SettingProvider { get; }
    protected AISettingConfiguration(ISettingProvider settingProvider)
    {
        SettingProvider = settingProvider;
    }
    public abstract Task<string> GetApiKeyAsync();
    public abstract Task<string> GetModelAsync();
   
    protected async Task<string> GetNotEmptySettingValueAsync(string name)
    {
        var value = await SettingProvider.GetOrNullAsync(name);

        if (value.IsNullOrEmpty())
        {
            throw new AbpException($"Setting value for '{name}' is null or empty!");
        }

        return value!;
    }

    

}