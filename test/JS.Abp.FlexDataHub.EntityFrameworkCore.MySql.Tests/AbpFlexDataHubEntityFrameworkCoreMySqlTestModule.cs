using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.MySql.Tests;
[DependsOn(typeof(AbpFlexDataHubEntityFrameworkCoreMySqlModule))]
public class AbpFlexDataHubEntityFrameworkCoreMySqlTestModule:AbpModule
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