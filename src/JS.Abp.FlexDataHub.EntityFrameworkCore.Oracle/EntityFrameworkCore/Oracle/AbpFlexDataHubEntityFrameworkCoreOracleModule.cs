using Volo.Abp.EntityFrameworkCore.Oracle;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Oracle;
[DependsOn(
    typeof(AbpFlexDataHubAbstractionModule),
    typeof(AbpEntityFrameworkCoreOracleModule)
)]
public class AbpFlexDataHubEntityFrameworkCoreOracleModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IOracleDynamicEntityRepository, OracleDynamicEntityRepository>();

        context.Services.AddAbpDbContext<OracleDbContext>(options =>
        {
        });
    }
}