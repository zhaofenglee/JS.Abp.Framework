using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Sqlite.Tests;
[DependsOn(typeof(AbpFlexDataHubEntityFrameworkCoreSqliteModule))]
public class AbpFlexDataHubEntityFrameworkCoreSqliteTestModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(abpDbContextConfigurationContext =>
            {
                abpDbContextConfigurationContext.DbContextOptions.UseSqlite();
            });
        });

        context.Services.Configure<FlexDataHubOptions>(options =>
        {
            options.LogToConsole = true;
            options.UseConfigConnectionString = true;
        });
    }
}