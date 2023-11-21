using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace JS.Abp.RestSharp;

public class AbpRestSharpModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(IRestSharpManager), typeof(RestSharpManager));
    }
}