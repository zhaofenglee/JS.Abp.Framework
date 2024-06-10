using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Oracle.Tests;
[DependsOn(typeof(AbpFlexDataHubEntityFrameworkCoreOracleModule))]
public class AbpFlexDataHubEntityFrameworkCoreOracleTestModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            
        });

        context.Services.Configure<FlexDataHubOptions>(options =>
        {
            options.LogToConsole = true;
            options.UseConfigConnectionString = true;
        });
    }
}