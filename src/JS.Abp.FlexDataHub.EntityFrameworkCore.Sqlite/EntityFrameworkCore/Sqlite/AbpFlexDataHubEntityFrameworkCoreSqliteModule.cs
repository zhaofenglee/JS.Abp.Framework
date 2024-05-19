using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Sqlite;
[DependsOn(
    typeof(AbpFlexDataHubAbstractionModule),
    typeof(AbpEntityFrameworkCoreSqliteModule)
)]
public class AbpFlexDataHubEntityFrameworkCoreSqliteModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ISqliteDynamicEntityRepository, SqliteDynamicEntityRepository>();

        context.Services.AddAbpDbContext<SqliteDbContext>(options =>
        {
        });
    }
}