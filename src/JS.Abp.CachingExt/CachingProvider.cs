using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.Json;

namespace JS.Abp.CachingExt;

public class CachingProvider:ICachingProvider
{
    private readonly IDistributedCache<DynamicCacheItem, string> _cache;
    protected IJsonSerializer JsonSerializer { get; }
    
    public CachingProvider(IDistributedCache<DynamicCacheItem, string> cache,IJsonSerializer jsonSerializer)
    {
        _cache = cache;
        JsonSerializer = jsonSerializer;
    }
    
    public void SetString(string key, string value, int expiryMinutes = 2)
    {
        _cache.Set(
            key, //Guid type used as the cache key
            new DynamicCacheItem()
            {
                Key = key,
                Value = value
            },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiryMinutes)
            }
        );
    }

    public async Task SetStringAsync(string key, string value, int expiryMinutes = 2)
    {
        await _cache.SetAsync(
            key, //Guid type used as the cache key
           new DynamicCacheItem()
               {
                   Key = key,
                   Value = value
               },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiryMinutes)
            }
        );
    }

    public string? GetString(string key)
    {
        var item = _cache.Get(
            key,//Cache key,
            false
        );
        return item?.Value;
    }

    public async Task<string?> GetStringAsync(string key)
    {
        var item = await _cache.GetAsync(
            key,//Cache key,
            false
        );
        return item?.Value;
    }

    public void Set<T>(string key, T value, int expiryMinutes = 2)
    {
        _cache.Set(
            key, //Guid type used as the cache key
            new DynamicCacheItem()
            {
                Key = $"{typeof(T).Name}_{key}",
                Value = JsonSerializer.Serialize(value)
            },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiryMinutes)
            }
        );
    }

    public async Task SetAsync<T>(string key, T value, int expiryMinutes = 2)
    {
       await _cache.SetAsync(
            key, //Guid type used as the cache key
            new DynamicCacheItem()
            {
                Key = $"{typeof(T).Name}_{key}",
                Value = JsonSerializer.Serialize(value)
            },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiryMinutes)
            }
        );
    }

    public void SetMany<T>(string key, T value, int expiryMinutes = 2)
    {
        //查询，不存在时写入，存在时追加
        var items = GetMany<T>(key);
        if (items == null)
        {
            var newList = new List<T>();
            newList.Add(value);
            Set(key, newList, expiryMinutes);
        }
        else
        {
            var list = items as IList<T>;
            if (list != null)
            {
                list.Add(value);
                Set(key, list, expiryMinutes);
            }
            else
            {
                var newList = new List<T>();
                newList.Add(value);
                Set(key, newList, expiryMinutes);
            }
        }
    }

    public async Task SetManyAsync<T>(string key, T value, int expiryMinutes = 2)
    {
        //查询，不存在时写入，存在时追加
        var items = await GetAsync<T>(key);
        if (items == null)
        {
            await SetAsync(key, value, expiryMinutes);
        }
        else
        {
            var list = items as IList<T>;
            if (list != null)
            {
                list.Add(value);
                await SetAsync(key, list, expiryMinutes);
            }
            else
            {
                await SetAsync(key, value, expiryMinutes);
            }
        }
    }

    public T? Get<T>(string key)
    {
        var item = _cache.Get(
            key,//Cache key,
            false
        );
        return item == null ? default : JsonSerializer.Deserialize<T>(item.Value);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var item =await _cache.GetAsync(
            key,//Cache key,
            false
        );
        return item == null ? default : JsonSerializer.Deserialize<T>(item.Value);
    }
    
    public bool Add<T>(string key, T value, int expiryMinutes = 2)
    {
        var item = Get<T>(key);
        if (item == null)
        {
            Set(key, value, expiryMinutes);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> AddAsync<T>(string key, T value, int expiryMinutes = 2)
    {
        var item = await GetAsync<T>(key);
        if (item == null)
        {
            await SetAsync(key, value, expiryMinutes);
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<T>? GetMany<T>(string hashId)
    {
        var item = _cache.Get(
            hashId,//Cache key,
            false
        );
        if (item == null)
        {
            return default;
        }
        else
        {
            var items =  JsonSerializer.Deserialize<List<T>>(item.Value);
            return items.ToList();
            
        }
        
    }

    public async Task<List<T>>? GetManyAsync<T>(string hashId)
    {
        var item = await _cache.GetAsync(
            hashId,//Cache key,
            false
        );
        if (item == null)
        {
            return default;
        }
        else
        {
            var items =  JsonSerializer.Deserialize<List<DynamicCacheItem>>(item.Value);
            return items == null ? default : items.Select(x=>JsonSerializer.Deserialize<T>(x.Value)).ToList();
            
        }
    }

    public bool AddMany<T>(string hashId,string key, T value, int expiryMinutes = 2)
    { 
        //查询，不存在时写入,存在时需判断重复
        var items = Get<List<DynamicCacheItem>>(hashId);
        if (items == null)
        {
            var newList = new List<DynamicCacheItem>();
            newList.Add(new DynamicCacheItem()
            {
                Key = key,
                Value = JsonSerializer.Serialize(value)
            });
            Set(hashId, newList, expiryMinutes);
            return true;
        }
        else
        {
            var list = items as List<DynamicCacheItem>;
            if (list != null)
            {
                //判断list是否包含value，存在返回false，不存在写入返回true
                if (list.Any(x=>x.Key==key))
                {
                    return false;
                }
                list.Add(new DynamicCacheItem()
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(value)
                });
                Set(hashId, list, expiryMinutes);
                return true;
                
            }
            else
            {
                var newList = new List<DynamicCacheItem>();
                newList.Add(new DynamicCacheItem()
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(value)
                });
                Set(hashId, newList, expiryMinutes);
                return true;
            }
        }
    }

    public async Task<bool> AddManyAsync<T>(string hashId,string key, T value, int expiryMinutes = 2)
    {
        //查询，不存在时写入,存在时需判断重复
        var items = await GetAsync<List<DynamicCacheItem>>(hashId);
        if (items == null)
        {
            var newList = new List<DynamicCacheItem>();
            newList.Add(new DynamicCacheItem()
            {
                Key = key,
                Value = JsonSerializer.Serialize(value)
            });
            await SetAsync(hashId, newList, expiryMinutes);
            return true;
        }
        else
        {
            var list = items as List<DynamicCacheItem>;
            if (list != null)
            {
                //判断list是否包含value，存在返回false，不存在写入返回true
                if (list.Any(x=>x.Key==key))
                {
                    return false;
                }
                list.Add(new DynamicCacheItem()
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(value)
                });
                await SetAsync(hashId, list, expiryMinutes);
                return true;
                
            }
            else
            {
                var newList = new List<DynamicCacheItem>();
                newList.Add(new DynamicCacheItem()
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(value)
                });
                await SetAsync(hashId, newList, expiryMinutes);
                return true;
            }
        }
    }

    public void Remove(string key)
    { 
        _cache.Remove(key, false);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key, false);
    }

    public void Refresh(string key)
    {
        _cache.Refresh(key,false);
    }

    public async Task RefreshAsync(string key)
    { 
        await _cache.RefreshAsync(key,false);
    }

    public void Update<T>(string key, T value, int expiryMinutes = 2)
    {
        //先删除，再添加
        Remove(key);
        Set(key, value, expiryMinutes);
    }

    public async Task UpdateAsync<T>(string key, T value, int expiryMinutes = 2)
    {
        //先删除，再添加
        await RemoveAsync(key);
        await SetAsync(key, value, expiryMinutes);
    }
}