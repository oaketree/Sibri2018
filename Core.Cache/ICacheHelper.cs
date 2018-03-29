using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache
{
    public interface ICacheHelper
    {
        object TryGetValueSet(string name, out object cacheEntry);
        Task<object> GetOrCreateAsync(string name, object cacheEntry);
        object GetOrCreate(string name, object cacheEntry);

    }
}
