using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.Azure;

[DependsOn(
    typeof(AbpJsonAbstractionsModule),
    typeof(AbpAIModule)
)]
public class AbpAIAzureModule: AbpModule
{
    
}