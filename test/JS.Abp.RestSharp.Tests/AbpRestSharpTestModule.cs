using Volo.Abp.Modularity;

namespace JS.Abp.RestSharp.Tests;
[DependsOn(typeof(AbpRestSharpModule))]
public class AbpRestSharpTestModule: AbpModule
{
    
}