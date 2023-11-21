namespace JS.Abp.CachingExt;

public interface ICachingProvider
{
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        void SetString(string key, string value, int expiryMinutes = 2);
        
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirySeconds"></param>
        Task SetStringAsync(string key, string value, int expiryMinutes = 2);
        
        /// <summary>
        /// 获取键值(String类型)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string? GetString(string key);
        /// <summary>
        /// 获取键值(String类型)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string?> GetStringAsync(string key);
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        void Set<T>(string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirySeconds"></param>
        Task SetAsync<T>(string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 批量设置键值，存在时追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        void SetMany<T>(string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 批量设置键值，存在时追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirySeconds"></param>
        Task SetManyAsync<T>(string key, T value, int expiryMinutes = 2);
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T? Get<T>(string key);
        
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(string key);
        
        /// <summary>
        /// 如果Key重复插入会报false异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        /// <returns></returns>
        bool Add<T>(string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 如果Key重复插入会报false异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        /// <returns></returns>
        Task<bool> AddAsync<T>(string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 批量获取键值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        List<T>? GetMany<T>(string hashId);
        
        /// <summary>
        /// 批量获取键值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        Task<List<T>>? GetManyAsync<T>(string hashId);
        
        /// <summary>
        /// 如果value重复插入会报false异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        bool AddMany<T>(string hashId,string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 如果value重复插入会报false异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirySeconds"></param>
        Task<bool> AddManyAsync<T>(string hashId,string key, T value, int expiryMinutes = 2);
        void Remove(string key);

        Task RemoveAsync(string key);
       
        void Refresh(string key);
        
        Task RefreshAsync(string key);
        /// <summary>
        /// 更新键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryMinutes"></param>
        void Update<T>(string key, T value, int expiryMinutes = 2);
        
        /// <summary>
        /// 更新键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirySeconds"></param>
        Task UpdateAsync<T>(string key, T value, int expiryMinutes = 2);
}