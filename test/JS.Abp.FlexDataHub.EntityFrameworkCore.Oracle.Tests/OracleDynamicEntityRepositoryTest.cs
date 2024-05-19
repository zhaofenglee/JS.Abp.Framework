using Shouldly;
using Volo.Abp.Testing;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Oracle.Tests;

public class OracleDynamicEntityRepositoryTest: AbpIntegratedTest<AbpFlexDataHubEntityFrameworkCoreOracleTestModule>
{
    private readonly IOracleDynamicEntityRepository _dynamicEntityRepository;

    public OracleDynamicEntityRepositoryTest()
    {
        _dynamicEntityRepository = GetRequiredService<IOracleDynamicEntityRepository>();
    }
    [Fact]
    public async Task ExecuteDynamicQueryAsyncTest()
    {
        var result = await _dynamicEntityRepository.ExecuteDynamicQueryAsync(
            connectionString:"Default",
            query:"SELECT * FROM \"AbpUsers\"",
            extraProperties: new Dictionary<string, object?>()
            {
                {"UserName","admin"}
            });
        result.ShouldNotBeNull();


    }
}