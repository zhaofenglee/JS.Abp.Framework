using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.SqlServer;

[DependsOn(
    typeof(AbpFlexDataHubAbstractionModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule)
)]
public class AbpFlexDataHubEntityFrameworkCoreSqlServerModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IMsSqlDynamicEntityRepository, MsSqlDynamicEntityRepository>();

        context.Services.AddAbpDbContext<SqlServerDbContext>(options =>
        {
        });
    }
}