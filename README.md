# JS.Abp.Framework
Abp Framework for ASP.NET Core

## JS.Abp.BarCode
仅需几行代码即可生成条码

## JS.Abp.Blazor.UI
在Blazorise基础上封装的一些组件

### 附件上传&下载模板
```
@using JS.Abp.BlazorUI.Components

<FileUploadModal @ref="FileUploadModal" Filter=".xlsx" OnFileUploaded="ImportExcelAsync" OnDownloadTemplateFile="DownloadAsExcelTemplateAsync" />
```

## JS.Abp.CachingExt
缓存扩展

## JS.Abp.MailKit
解决MailKit发送邮件时邮箱服务器证书问题

## JS.Abp.RestSharp
RestSharp扩展

## JS.Abp.AI.OpenAI
OpenAI ChatGpt第三方组件请求封装

## JS.Abp.AI.DashScope
阿里DashScopeHttp请求封装

## JS.Abp.FlexDataHub
实现使用SQL查询数据的方式
```csharp
//根据所用的数据库类型，选择对应的仓储
private readonly IMySqlDynamicEntityRepository _dynamicEntityRepository;
//连接字符串默认取appsettings.json中的ConnectionStrings
var result = await _dynamicEntityRepository.ExecuteDynamicQueryAsync(
            connectionString:"Default",
            query:"SELECT * FROM AbpUsers",
            extraProperties: new Dictionary<string, object?>()
            {
                {"UserName","admin"}
            });
```

