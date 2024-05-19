using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.MySql;

public class MySqlDbContext: AbpDbContext<MySqlDbContext>
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}