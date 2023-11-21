using Shouldly;
using Volo.Abp.Testing;

namespace JS.Abp.RestSharp.Tests;

public class RestSharpManagerTest: AbpIntegratedTest<AbpRestSharpTestModule>
{
    
    
    [Fact]
    public async Task Execute_Test1()
    {
        var restSharp = GetRequiredService<IRestSharpManager>();
        var result = await restSharp.ExecuteAsync("https://www.baidu.com");
        result.IsSuccessful.ShouldBeTrue();
    }
}