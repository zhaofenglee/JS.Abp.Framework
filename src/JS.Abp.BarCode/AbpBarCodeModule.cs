using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace JS.Abp.BarCode
{
    public class AbpBarCodeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton(typeof(IBarCodeHelper), typeof(BarCodeHelper));
        }
    }
}
