using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.PostgreSql;
[DependsOn(
    typeof(AbpFlexDataHubAbstractionModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
)]
public class AbpFlexDataHubEntityFrameworkCorePostgreSqlModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IPostgreSqlDynamicEntityRepository, PostgreSqlDynamicEntityRepository>();

        context.Services.AddAbpDbContext<PostgreSqlDbContext>(options =>
        {
        });
    }
}