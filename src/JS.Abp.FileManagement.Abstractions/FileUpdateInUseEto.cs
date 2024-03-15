using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace JS.Abp.FileManagement.Abstractions;

[Serializable]
[EventName("JS.Abp.FileManagement.FileUpdateInUse")]
public class FileUpdateInUseEto: IMultiTenant
{
    public Guid? TenantId { get; set; }
    
    public Guid Id { get; set; }
}