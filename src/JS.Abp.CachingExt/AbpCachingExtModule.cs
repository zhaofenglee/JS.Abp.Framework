using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace JS.Abp.CachingExt;

[DependsOn(
    typeof(AbpCachingModule))]
public class AbpCachingExtModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(ICachingProvider), typeof(CachingProvider));
    }
}