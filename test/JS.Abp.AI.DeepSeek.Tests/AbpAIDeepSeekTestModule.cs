using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.DeepSeek.Tests;

[DependsOn(typeof(AbpJsonSystemTextJsonModule),
    typeof(AbpAIDeepSeekModule))]
public class AbpAIDeepSeekTestModule:AbpModule
{
    
}