using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.OpenAI;

[DependsOn(
    typeof(AbpJsonAbstractionsModule),
    typeof(AbpAIModule)
)]
public class AbpAIOpenAIModule: AbpModule
{
}