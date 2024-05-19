namespace JS.Abp.FlexDataHub;

public class FlexDataHubOptions
{
    /// <summary>
    /// 输出日志到命令行
    /// </summary>
    public bool LogToConsole { get; set; }

    /// <summary>
    /// 从配置文件获取连接字符串
    /// </summary>
    public bool UseConfigConnectionString { get; set; }

    
    public FlexDataHubOptions()
    {
        LogToConsole = false;
        UseConfigConnectionString = true;
    }
}