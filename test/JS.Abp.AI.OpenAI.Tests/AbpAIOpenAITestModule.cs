using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.OpenAI.Tests;

[DependsOn(typeof(AbpJsonSystemTextJsonModule),
    typeof(AbpAIOpenAIModule))]
public class AbpAIOpenAITestModule:AbpModule
{
    
}