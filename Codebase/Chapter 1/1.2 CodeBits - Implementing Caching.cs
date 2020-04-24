// Absolute Expiration
[TestCase("Cache1", 1)]
[TestCase("Cache1", 2)]
[TestCase("Cache1", 3)]
public void CanCache(string key, int value)
{

    // ARRANGE
    ObjectCache cache = MemoryCache.Default;

    // Absolute - DATETIME 
    CacheItemPolicy policy = new CacheItemPolicy
    {
        AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1))
    };

    // ACT
    cache.Remove(key);
    // Sets Cache
    cache.Add(key, value, policy);

    // Retrieve the value
    int fetchedValue = (int)cache.Get(key);

    // ASSERT -- testing
    Assert.That(fetchedValue, Is.EqualTo(value), "Uh oh!");
}

//  Sliding Expiration
[TestCase("Sliding1", 1)]
[TestCase("Sliding2", 2)]
[TestCase("Sliding3", 3)]
public void TestSlidingExpiration(string key, int value)
{
    // ARRANGE
    ObjectCache cache = MemoryCache.Default;

    CacheItemPolicy policy = new CacheItemPolicy
    {
        SlidingExpiration = new TimeSpan(0, 0, 2)
    };

    cache.Set(key, value, policy);

    // ACT
    for (var i = 0; i < 22; i++)
    {
        System.Threading.Thread.Sleep(100);
        Assume.That(cache.Get(key), Is.EqualTo(value));
    }

    System.Threading.Thread.Sleep(2001);
    
    // ASSERT
    Assert.That(cache.Get(key), Is.Null, "Uh oh!");
}