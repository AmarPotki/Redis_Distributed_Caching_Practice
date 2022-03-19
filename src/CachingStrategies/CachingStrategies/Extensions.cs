using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingStrategies
{
    public static class Extensions
    {
        public static IDistributedCache ToNameSpaced(this IDistributedCache cache, string name) => new NamespacedCache(cache, name);
    }
}
