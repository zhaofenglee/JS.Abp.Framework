﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace JS.Abp.AI.DashScope;

public class DashScopeSettingConfiguration:AISettingConfiguration,IDashScopeSettingConfiguration,ITransientDependency
{
    public DashScopeSettingConfiguration(ISettingProvider settingProvider) : base(settingProvider)
    {
    }
    
    public override Task<string?> GetApiKeyAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.DashScope.ApiKey);
    }
    
    public override Task<string?> GetModelAsync()
    {
        return GetOrNullSettingValueAsync(AISettingNames.DashScope.Model);
    }
   
}