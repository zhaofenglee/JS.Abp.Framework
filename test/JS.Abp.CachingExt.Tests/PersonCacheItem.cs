namespace JS.Abp.CachingExt.Tests;

public class PersonCacheItem
{
    public string Name { get; private set; }

    private PersonCacheItem()
    {

    }

    public PersonCacheItem(string name)
    {
        Name = name;
    }
}