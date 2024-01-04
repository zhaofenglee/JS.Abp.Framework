using Volo.Abp.Data;

namespace JS.Abp.FileManagement.Abstractions;

public interface IFileData : IHasExtraProperties
{
    Guid? TenantId { get; }
    Guid ProjectId { get; }
    Guid FileDescriptorId { get; }
    Guid DirectoryId { get; }
    string? Project { get; }
    string Name { get; }
    int Size { get; }
    string? MimeType { get; }
    
    Guid? CreateUser { get; }
    //是否使用
    //bool IsUsed { get; }
}