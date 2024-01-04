using Volo.Abp.EventBus;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace JS.Abp.FileManagement.Abstractions;

[DependsOn(
    typeof(AbpMultiTenancyModule),
    typeof(AbpEventBusModule)
)]
public class AbpFileManagementAbstractionModule:AbpModule
{
    
}