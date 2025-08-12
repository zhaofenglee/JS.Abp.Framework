using Volo.Abp.EntityFrameworkCore.MySQL;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.MySql;

[DependsOn(
    typeof(AbpFlexDataHubAbstractionModule),
    typeof(AbpEntityFrameworkCoreMySQLPomeloModule)
)]
public class AbpFlexDataHubEntityFrameworkCoreMySqlModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IMySqlDynamicEntityRepository, MySqlDynamicEntityRepository>();

        context.Services.AddAbpDbContext<MySqlDbContext>(options =>
        {
        });
    }
}