using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Cico.Models.Helpers
{
    public static class CacheHelper
    {
        public static T Cache<T>(Func<T> fn, string key) where T : class
        {
            var k = typeof(T).Name + "_" + key;
            if (HttpContext.Current.Cache[k] == null)
            {
                var res = fn.Invoke();
                if (res == null)
                {
                    return null;
                }
                HttpContext.Current.Cache[k] = fn.Invoke();
            }
            return HttpContext.Current.Cache[k] as T;
        }

        public static T Cache<T>(Func<T> fn, string key, TimeSpan timeSpan) where T : class
        {
            var k = typeof(T).Name + "_" + key;
            if (HttpContext.Current.Cache[k] == null)
            {
                var res = fn.Invoke();
                if (res == null)
                    return null;
                HttpContext.Current.Cache.Add(k, res, null, DateTime.Now + timeSpan, System.Web.Caching.Cache.NoSlidingExpiration,
                                              CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache[k] as T;
        }
    }
}