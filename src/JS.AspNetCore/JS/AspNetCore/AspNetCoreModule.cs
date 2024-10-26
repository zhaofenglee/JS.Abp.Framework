using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace JS.AspNetCore;

[DependsOn(typeof(Volo.Abp.Http.AbpHttpModule),
    typeof(AbpJsonAbstractionsModule),
    typeof(JS.Abp.Http.AbpHttpAbstractionsModule))]
public class AspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
    }
}