using Volo.Abp.Data;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace JS.Abp.FileManagement.Abstractions;

[EventName("JS.Abp.FileManagement.File")]
public class FileEto : IFileData,IMultiTenant
{
    public Guid ProjectId { get; set; }
    public Guid FileDescriptorId { get; set; }
    public Guid DirectoryId { get; set; }
    public string? Project { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public string? MimeType { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? CreateUser { get; set; }
    //public bool IsUsed { get; set; }
    
    public ExtraPropertyDictionary ExtraProperties { get; set; }
}