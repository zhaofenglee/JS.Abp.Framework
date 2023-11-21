using Volo.Abp.Caching;

namespace JS.Abp.CachingExt;

[CacheName("DynamicCache")]
public class DynamicCacheItem
{
    public string? Key { get; set; }

    public string? Value { get; set; }

}