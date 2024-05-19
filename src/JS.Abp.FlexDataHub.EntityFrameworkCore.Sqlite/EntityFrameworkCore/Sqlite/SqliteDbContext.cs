using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Sqlite;

public class SqliteDbContext: AbpDbContext<SqliteDbContext>
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}