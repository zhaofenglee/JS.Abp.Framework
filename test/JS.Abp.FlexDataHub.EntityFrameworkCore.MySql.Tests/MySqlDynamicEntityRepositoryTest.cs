using Shouldly;
using Volo.Abp.Testing;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.MySql.Tests;

public class MySqlDynamicEntityRepositoryTest: AbpIntegratedTest<AbpFlexDataHubEntityFrameworkCoreMySqlTestModule>
{
    private readonly IMySqlDynamicEntityRepository _dynamicEntityRepository;

    public MySqlDynamicEntityRepositoryTest()
    {
        _dynamicEntityRepository = GetRequiredService<IMySqlDynamicEntityRepository>();
    }
    [Fact]
    public async Task ExecuteDynamicQueryAsyncTest()
    {
        var result = await _dynamicEntityRepository.ExecuteDynamicQueryAsync(
            connectionString:"Default",
            query:"SELECT * FROM AbpUsers",
            extraProperties: new Dictionary<string, object?>()
            {
                {"UserName","admin"}
            });
        result.ShouldNotBeNull();


    }
}