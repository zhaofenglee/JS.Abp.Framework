using Volo.Abp.Emailing;
using Volo.Abp.MailKit;
using Volo.Abp.Modularity;

namespace JS.Abp.MailKitExt
{
    [DependsOn(typeof(AbpMailKitModule))]
    public class AbpMailKitExtModule : AbpModule
    {

    }
}