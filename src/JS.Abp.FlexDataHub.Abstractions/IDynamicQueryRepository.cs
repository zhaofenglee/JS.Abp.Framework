namespace JS.Abp.FlexDataHub;

public interface IDynamicQueryRepository
{
    /// <summary>
    /// 动态查询
    /// </summary>
    /// <param name="connectionString">数据库连接</param>
    /// <param name="query">查询语句</param>
    /// <param name="filterText">过滤条件</param>
    /// <param name="extraProperties">参数</param>
    /// <param name="parameters">自定义参数</param>
    /// <param name="groupBy"></param>
    /// <param name="sorting"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<DynamicEntityData>> ExecuteDynamicQueryAsync(string? connectionString,
        string query,
        string? filterText = null,
        Dictionary<string, object?>? extraProperties = null,
        string? parameters = null,
        string? groupBy = null,
        string? sorting = null,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<DynamicEntityData>> ExecuteDynamicProcedureAsync(string? connectionString,
        string query,
        Dictionary<string, object?>? extraProperties = null,
        CancellationToken cancellationToken = default);
}