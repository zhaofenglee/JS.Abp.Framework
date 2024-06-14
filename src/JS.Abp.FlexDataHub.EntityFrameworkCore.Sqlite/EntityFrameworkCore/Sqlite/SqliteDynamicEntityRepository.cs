using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace JS.Abp.FlexDataHub.EntityFrameworkCore.Sqlite;

public class SqliteDynamicEntityRepository: ISqliteDynamicEntityRepository
{
    protected FlexDataHubOptions Options { get; }
    public SqliteDynamicEntityRepository(SqliteDbContext dbContextProvider,
        IOptions<FlexDataHubOptions> options)
    {
        Options = options.Value;
    }
    
    public async Task<IEnumerable<DynamicEntityData>> ExecuteDynamicQueryAsync(
        string? connectionString,
        string query,
        string? filterText = null,
        Dictionary<string, object?>? extraProperties = null,
        string? parameters = null,
        string? groupBy = null,
        string? sorting = null,
        CancellationToken cancellationToken = default) 
    {
        using (var dbContext = await OpenDatabaseConnectionAsync(connectionString, cancellationToken))
        {
            using (var command = await CreateCommand(dbContext,query, CommandType.Text,filterText,extraProperties,parameters,groupBy,sorting,cancellationToken))
            {
                using (var dataReader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    var result = new List<DynamicEntityData>();
                    int row = 1;

                    while (await dataReader.ReadAsync(cancellationToken))
                    {
                        //行循环写入
                        var entity = new DynamicEntityData();
                        entity.Row = row++;
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            var value = dataReader[i] == DBNull.Value ? null : dataReader[i];
                            entity.Data.Add(dataReader.GetName(i) ,value);
                        }
                        result.Add(entity);
                    }
                    return result;
                }
            } 
        }
        
        

    }

    public async Task<IEnumerable<DynamicEntityData>> ExecuteDynamicProcedureAsync(string query, string? connectionString,
        Dictionary<string, object?>? extraProperties = null, CancellationToken cancellationToken = default)
    {
        using (var dbContext = await OpenDatabaseConnectionAsync(connectionString, cancellationToken))
        {
            using (var command = await CreateCommand(dbContext,query, CommandType.Text,extraProperties,cancellationToken))
            {
                using (var dataReader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    var result = new List<DynamicEntityData>();
                    int row = 1;

                    while (await dataReader.ReadAsync(cancellationToken))
                    {
                        //行循环写入
                        var entity = new DynamicEntityData();
                        entity.Row = row++;
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            entity.Data.Add(dataReader.GetName(i) ,dataReader[i]);
                        }
                        result.Add(entity);
                    }
                    return result;
                }
            }
        }
       
    }
 private async Task<DbCommand> CreateCommand(SqliteDbContext dbContext,
        string commandText,
        CommandType commandType,
        Dictionary<string, object?>? extraProperties = null,
        CancellationToken cancellationToken = default)
    {
        //var dbContext = await OpenDatabaseConnectionAsync(connectionString, cancellationToken);
        var command = dbContext.Database.GetDbConnection().CreateCommand();

        command.CommandText = commandText;
        command.CommandType = commandType;
        command.Transaction = dbContext.Database.CurrentTransaction?.GetDbTransaction();
        if (extraProperties!=null)
        {
            foreach (var dist in extraProperties)
            {
                command.Parameters.Add(new SqliteParameter($"@{dist.Key}",dist.Value));
            }
         
        }
        return command;

    }
    private async Task<DbCommand> CreateCommand(SqliteDbContext dbContext,
        string commandText,
        CommandType commandType,
        string? filterText = null,
        Dictionary<string, object?>? extraProperties = null,
        string? parameters = null,
        string? groupBy = null,
        string? sorting = null,
        CancellationToken cancellationToken = default)
    {
        //var dbContext = await OpenDatabaseConnectionAsync(connectionString, cancellationToken);
        var command = dbContext.Database.GetDbConnection().CreateCommand();

       
        command.CommandType = commandType;
        command.Transaction = dbContext.Database.CurrentTransaction?.GetDbTransaction();
        if (extraProperties!=null)
        {
            string where1 = "";
            string where2 = "";
            foreach (var dist in extraProperties)
            {
                if (!filterText.IsNullOrWhiteSpace())
                {
                    string key = dist.Key.Replace("Min", "").Replace("Max", "");
                    if (where1.IsNullOrWhiteSpace())
                    {
                        where1 += $" {key} LIKE '%'+ @filter + '%' ";
                    }
                    else
                    {
                        where1 += $" OR {key} LIKE '%'+ @filter + '%' ";
                    }
                }
                
                if (dist.Value==null||dist.Value.ToString().IsNullOrWhiteSpace())
                {
                    continue;
                }

                if (dist.Key.Contains("Min"))
                {
                    where2 += $" AND {dist.Key.Replace("Min","")} >=@{dist.Key.Replace(".","")} ";
                    command.Parameters.Add(new SqliteParameter(dist.Key.Replace(".",""),dist.Value));
                }
                else if (dist.Key.Contains("Max"))
                {
                    where2 += $" AND {dist.Key.Replace("Max","")} <=@{dist.Key.Replace(".","")} ";
                    command.Parameters.Add(new SqliteParameter(dist.Key.Replace(".",""),dist.Value));
                }
                else
                {
                    where2 += $" AND {dist.Key} =@{dist.Key.Replace(".","")} ";
                    command.Parameters.Add(new SqliteParameter(dist.Key.Replace(".",""),dist.Value));
                }
              
                
            }
            if (!where1.IsNullOrWhiteSpace())
            {
                command.CommandText += $" AND ({where1}) ";
            }
            if (!where2.IsNullOrWhiteSpace())
            {
                command.CommandText += where2;
            }
        }
        if (!filterText.IsNullOrWhiteSpace())
            command.Parameters.Add(new SqliteParameter("filter",filterText));
        if(!parameters.IsNullOrWhiteSpace())
        {
            command.CommandText += $" AND {parameters}";
        }
        // foreach (var parameter in parameters)
        // {
        //     command.Parameters.Add(parameter);
        // }
        command.CommandText += $" {groupBy} {sorting} ";
        if (!command.CommandText.IsNullOrWhiteSpace())
        {
            command.CommandText = commandText +" WHERE 1=1 " + command.CommandText;
        }
        else
        {
            command.CommandText = commandText;
        }
        if (Options.LogToConsole)
        {
            Console.WriteLine(command.CommandText);
        }
     
        return command;

    }
    
    private async Task<SqliteDbContext> OpenDatabaseConnectionAsync(string connectionString, CancellationToken cancellationToken = default)
    {
        string finalConnectionString = Options.UseConfigConnectionString ? BuildConfiguration().GetConnectionString(connectionString) : connectionString;

        var dbContextOptions = new DbContextOptionsBuilder<SqliteDbContext>()
            .UseSqlite(finalConnectionString)
            .Options;

        var dbContext = new SqliteDbContext(dbContextOptions);
        await dbContext.Database.OpenConnectionAsync(cancellationToken); // 打开数据库连接

        return dbContext;
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }

}