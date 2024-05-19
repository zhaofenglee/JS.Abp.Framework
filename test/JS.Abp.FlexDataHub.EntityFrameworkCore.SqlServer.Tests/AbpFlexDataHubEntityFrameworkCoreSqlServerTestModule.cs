using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.SqlServer.Tests;

[DependsOn(typeof(AbpFlexDataHubEntityFrameworkCoreSqlServerModule))]
public class AbpFlexDataHubEntityFrameworkCoreSqlServerTestModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(abpDbContextConfigurationContext =>
            {
                abpDbContextConfigurationContext.DbContextOptions.UseSqlServer();
            });
        });

        context.Services.Configure<FlexDataHubOptions>(options =>
        {
            options.LogToConsole = true;
            options.UseConfigConnectionString = true;
        });
    }
}