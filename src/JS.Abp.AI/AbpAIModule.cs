using JS.Abp.AI.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.AI;
[DependsOn(typeof(AbpSettingsModule),
    typeof(AbpVirtualFileSystemModule),
    typeof(AbpLocalizationModule))]
public class AbpAIModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAIModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AIResource>("en")
                .AddVirtualJson("/Localization");
        });
    }
}