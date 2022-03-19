﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace CachingStrategies
{
    public class WriteBackCache : IDistributedCache
    {
        private readonly IDistributedCache _main;
        private readonly IDistributedCache _secondary;

        public WriteBackCache(IDistributedCache main, IDistributedCache secondary)
        {
            _main = main;
            _secondary = secondary;
            _backgroundTask = Task.Run(WriteBack);
        }

        private async Task WriteBack()
        {
            try
            {
                while (true)
                {
                    if (writeToBuffer.Count>100)
                    {
                        //update main cache
                    }

                    await Task.Delay(1000 * 60);
                }
            }
            catch (Exception e)
            {
                
            }
        }

        public byte[] Get(string key)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        private List<KeyValuePair<string, byte[]>> writeToBuffer = new();
        private readonly Task _backgroundTask;

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            _secondary.Set(key, value);
            writeToBuffer.Add(KeyValuePair.Create(key, value));

        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
