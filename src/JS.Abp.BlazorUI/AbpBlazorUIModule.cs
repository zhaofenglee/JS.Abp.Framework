using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.Client;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.BlazorUI;
[DependsOn(
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpHttpClientModule)
)]
[DependsOn(typeof(AbpLocalizationModule))]
public class AbpBlazorUIModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        
        context.Services.AddAutoMapperObjectMapper<AbpBlazorUIModule>();
       
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<BlazorUIAutoMapperProfile>();
        });


        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpBlazorUIModule).Assembly);
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpBlazorUIModule>();
        });
        Configure<AbpLocalizationOptions>(options =>
        {
            // options.Resources
            //     .Add<BlazorUIResource>("en")
            //     .AddVirtualJson("/Localization/BlazorUI");
            options.Resources
                .Get<AbpUiResource>()
                .AddVirtualJson("/Localization/Identity");

        });
        // Configure<AbpExceptionLocalizationOptions>(options =>
        // {
        //     options.MapCodeNamespace("JS.Abp.BlazorUI", typeof(BlazorUIResource));
        // });
    }
}