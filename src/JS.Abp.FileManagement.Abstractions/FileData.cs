using Volo.Abp.Data;

namespace JS.Abp.FileManagement.Abstractions;

public class FileData : IFileData
{
    public Guid? TenantId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid FileDescriptorId { get; set; }
    public Guid DirectoryId { get; set; }
    public string? Project { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public string? MimeType { get; set; }
    
    public Guid? CreateUser { get; set; }
    //public bool IsUsed { get; set; }
    public ExtraPropertyDictionary ExtraProperties { get; }

    public FileData()
    {
    }

    public FileData(IFileData fileData)
    {
        TenantId = fileData.TenantId;
        ProjectId = fileData.ProjectId;
        FileDescriptorId = fileData.FileDescriptorId;
        DirectoryId = fileData.DirectoryId;
        Project = fileData.Project;
        Name = fileData.Name;
        Size = fileData.Size;
        MimeType = fileData.MimeType;
        CreateUser = fileData.CreateUser;
        //IsUsed = fileData.IsUsed;
        ExtraProperties = fileData.ExtraProperties;
    }

    public FileData(Guid projectId,
        Guid fileDescriptorId,
        Guid directoryId,
        string name,
        int size,
        //bool isUsed,
        string? project = null,
        string? mimeType = null,
        Guid? tenantId = null,
        Guid? createUser = null,
        ExtraPropertyDictionary extraProperties = null)
    {
        TenantId = tenantId;
        ProjectId = projectId;
        FileDescriptorId = fileDescriptorId;
        DirectoryId = directoryId;
        Project = project;
        Name = name;
        Size = size;
        //IsUsed = isUsed;
        MimeType = mimeType;
        CreateUser = createUser;
        ExtraProperties = extraProperties;
    }
}