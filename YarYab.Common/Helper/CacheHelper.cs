using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Memory;

namespace YarYab.Common.Helper
{
    public static class CacheHelper
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(5);
        private static readonly string _itemsKeyTemplate = "cache-{0}-{1}";

        public static string GenerateCacheKey(string[] Param, [CallerMemberName] string memberName = "MethodWithNoName")
        {
            string key = string.Format(_itemsKeyTemplate, memberName, String.Join(',', Param));
            return key;
        }
         public static void AddToCacheList( IMemoryCache cache ,string key)
        {
            List<string> CacheList;
            CacheList = (List<string>)cache.Get("CacheList") ?? new List<string>();
            CacheList.Add(key);
            cache.Set("CacheList", CacheList);
        }
        public static void ClearCache(IMemoryCache cache )
        {
            List<string> CacheList;
            CacheList = (List<string>)cache.Get("CacheList") ?? new List<string>();
            foreach (var item in CacheList)
            {
            cache.Remove(item);
             }
            CacheList.Clear();  
            cache.Set("CacheList", CacheList);

        }
         public static void SetCache<T>(IMemoryCache cache ,string[] param ,dynamic data)
        {
            cache.Set(GenerateCacheKey(param), (T)data, DefaultCacheDuration);
            AddToCacheList(cache, GenerateCacheKey(param));
        }
        public static T GetFromCache<T>(IMemoryCache cache, string[] param)
        {
            return  (T)cache.Get(CacheHelper.GenerateCacheKey(param));
        }
    }
}
