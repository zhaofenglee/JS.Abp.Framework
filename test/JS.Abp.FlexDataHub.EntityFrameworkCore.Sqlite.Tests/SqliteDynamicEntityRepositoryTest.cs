using JS.Abp.FlexDataHub.EntityFrameworkCore.Sqlite.Tests;
using Shouldly;
using Volo.Abp.Testing;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.SqlServer.Tests;

public class SqliteDynamicEntityRepositoryTest: AbpIntegratedTest<AbpFlexDataHubEntityFrameworkCoreSqliteTestModule>
{
    private readonly ISqliteDynamicEntityRepository _dynamicEntityRepository;

    public SqliteDynamicEntityRepositoryTest()
    {
        _dynamicEntityRepository = GetRequiredService<ISqliteDynamicEntityRepository>();
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