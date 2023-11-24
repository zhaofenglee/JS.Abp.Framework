using Volo.Abp.Testing;
using Shouldly;

namespace JS.Abp.CachingExt.Tests;

public class CachingProvider_Test: AbpIntegratedTest<AbpCachingExtTestModule>
{
    [Fact]
    public void Should_Set_Get_And_Remove_Cache_Items()
    {
        var caching = GetRequiredService<ICachingProvider>();
        var cacheKey = Guid.NewGuid().ToString();
        //Set
        caching.SetString(cacheKey, "test");
        //Get
        var cacheItem = caching.GetString(cacheKey);
        cacheItem.ShouldBe("test");
        //Remove    
        caching.Remove(cacheKey);
        
        var cacheKey2 = Guid.NewGuid().ToString();
        PersonCacheItem personCacheItem = new PersonCacheItem("test");
        //Set
        caching.Set<PersonCacheItem>(cacheKey2,personCacheItem);
        //Get
        var cacheItem2 = caching.Get<PersonCacheItem>(cacheKey2);
        cacheItem2.Name.ShouldBe("test");
        //Remove    
        caching.Remove(cacheKey2);
       
        var cacheKey3 = Guid.NewGuid().ToString();
        //Set
        caching.SetMany<PersonCacheItem>(cacheKey3,new PersonCacheItem("test1"));
        
        //Get
        var cacheItem3 = caching.GetMany<PersonCacheItem>(cacheKey3);
        cacheItem3.Count.ShouldBe(1);
        //Add 
        caching.SetMany<PersonCacheItem>(cacheKey3,new PersonCacheItem("test2"));
        
        //Get
        cacheItem3 = caching.GetMany<PersonCacheItem>(cacheKey3);
        cacheItem3.Count.ShouldBe(2);
        
    }
    
    [Fact]
    public void Should_Add_Items()
    {
        var caching = GetRequiredService<ICachingProvider>();
        var cacheKey = Guid.NewGuid().ToString();
        
        PersonCacheItem personCacheItem = new PersonCacheItem("test");
        //Set
        var isSucceed =  caching.Add<PersonCacheItem>(cacheKey,personCacheItem);
        //Get
        var cacheItem = caching.Get<PersonCacheItem>(cacheKey);
        
        //Set
        var isSucceed2 =  caching.Add<PersonCacheItem>(cacheKey,new PersonCacheItem("test2"));
        //Get
        var cacheItem2 = caching.Get<PersonCacheItem>(cacheKey);

    }
    
    [Fact]
    public void Should_AddMany_Items()
    {
        var caching = GetRequiredService<ICachingProvider>();
        var hashKey = Guid.NewGuid().ToString();
        
        PersonCacheItem personCacheItem = new PersonCacheItem("test");
        //Set
        var isSucceed =  caching.AddMany<PersonCacheItem>(hashKey,"Test",personCacheItem);
        //Get
        var cacheItem = caching.GetMany<PersonCacheItem>(hashKey);
        
        //Set
        var isSucceed2 =  caching.AddMany<PersonCacheItem>(hashKey,"Test",personCacheItem);
        //Get
        var cacheItem2 = caching.GetMany<PersonCacheItem>(hashKey);
        
        //Set
        var isSucceed3 =  caching.AddMany<PersonCacheItem>(hashKey,"Test3",personCacheItem);
        //Get
        var cacheItem3 = caching.GetMany<PersonCacheItem>(hashKey);

    }
}