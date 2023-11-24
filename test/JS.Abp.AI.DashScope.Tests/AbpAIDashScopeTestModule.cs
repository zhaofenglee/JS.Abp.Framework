using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.DashScope.Tests;

[DependsOn(typeof(AbpJsonSystemTextJsonModule),
    typeof(AbpAIDashScopeModule))]
public class AbpAIDashScopeTestModule:AbpModule
{
    
}