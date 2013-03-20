using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Hero
{
    /// <summary>
    /// Normally its not good practice to implement Singleton patterns
    /// as they make unit testing difficult.  In this case though we need
    /// to be sure that there is only one method of access to this data
    ///
    /// Implemented to be thread-safe
    /// Do not unseal and do not remove the
    /// initialition of TokenAuthenticationCache
    /// </summary>
    public sealed class TokenAuthenticationCache
    {
        private readonly Cache _authenticationCache;
        private static readonly TokenAuthenticationCache _instance = new TokenAuthenticationCache();

        private TokenAuthenticationCache()
        {
            _authenticationCache = HttpRuntime.Cache;
        }

        public static TokenAuthenticationCache Instance
        {
            get { return _instance; }
        }

        public void Insert(string key)
        {
            _authenticationCache.Insert(key, key, null,
                 Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(15));
        }

        public void Insert(string key, object objectToCache)
        {
            _authenticationCache.Insert(key, objectToCache, null,
                 Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(15));
        }

        public bool Validate(string token)
        {
            return _authenticationCache.Get(token) != null;
        }

        public object GetCachedObject(string token)
        {
            return _authenticationCache.Get(token);
        }

        public void Delete(string key)
        {
            _authenticationCache.Remove(key);
        }
    }
}
