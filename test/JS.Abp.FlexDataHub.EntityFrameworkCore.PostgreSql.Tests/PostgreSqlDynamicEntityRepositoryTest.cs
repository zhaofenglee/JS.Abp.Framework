using JS.Abp.FlexDataHub.EntityFrameworkCore.PostgreSql.Tests;
using Shouldly;
using Volo.Abp.Testing;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Oracle.Tests;

public class PostgreSql : AbpIntegratedTest<AbpFlexDataHubEntityFrameworkCorePostgreSqlTestModule>
{
    private readonly IPostgreSqlDynamicEntityRepository _dynamicEntityRepository;

    public PostgreSql()
    {
        _dynamicEntityRepository = GetRequiredService<IPostgreSqlDynamicEntityRepository>();
    }

    [Fact]
    public async Task ExecuteDynamicQueryAsyncTest()
    {
        var result = await _dynamicEntityRepository.ExecuteDynamicQueryAsync(
            connectionString: "Default",
            query: "SELECT * FROM \"EventHub\".public.\"AbpUsers\"",
            extraProperties: new Dictionary<string, object?>()
            {
                { "UserName", "admin" }
            });
        result.ShouldNotBeNull();
    }
}