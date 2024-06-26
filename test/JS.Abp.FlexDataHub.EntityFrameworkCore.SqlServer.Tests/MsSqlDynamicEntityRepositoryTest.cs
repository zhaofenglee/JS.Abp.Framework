using Shouldly;
using Volo.Abp.Testing;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.SqlServer.Tests;

public class MsSqlDynamicEntityRepositoryTest: AbpIntegratedTest<AbpFlexDataHubEntityFrameworkCoreSqlServerTestModule>
{
    private readonly IMsSqlDynamicEntityRepository _dynamicEntityRepository;

    public MsSqlDynamicEntityRepositoryTest()
    {
        _dynamicEntityRepository = GetRequiredService<IMsSqlDynamicEntityRepository>();
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

    [Fact]
    public async Task ExecuteDynamicProcedureAsyncTest()
    {
        var result = await _dynamicEntityRepository.ExecuteDynamicProcedureAsync(
            connectionString:"Default",
            query:"SearchAbpAuditLogs",
            extraProperties: new Dictionary<string, object?>()
            {
                {"ApplicationName",""},
                {"UserName",""}
            });
        result.ShouldNotBeNull();


    }
    
}