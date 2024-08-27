using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.Azure.Tests;
[DependsOn(typeof(AbpJsonSystemTextJsonModule),
    typeof(AbpAIAzureModule))]
public class AbpAIAzureTestModule:AbpModule
{
    
}