using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.SqlServer;

public class SqlServerDbContext: AbpDbContext<SqlServerDbContext>
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}