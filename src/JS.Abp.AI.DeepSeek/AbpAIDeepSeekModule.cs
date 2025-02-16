using JS.Abp.RestSharp;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace JS.Abp.AI.DeepSeek;

[DependsOn(
    typeof(AbpJsonAbstractionsModule),
    typeof(AbpAIModule),
    typeof(AbpRestSharpModule)
)]
public class AbpAIDeepSeekModule: AbpModule
{
}