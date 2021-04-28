using PublisherService.Services.Interfaces;
using System.Threading.Tasks;
using System;
using CommonRedis;

namespace PublisherService.Services
{
    public class RedisPublisher : IRedisPublisher
    {            
        public async Task<bool> Publish(string msg)
        {

            if (string.IsNullOrEmpty(msg))  return false;

            try
            {
                var pub = RedisStore.RedisCache.Multiplexer.GetSubscriber();
                var count = await pub.PublishAsync("testChannel", $"Date: {DateTime.UtcNow} : Message : {msg}");
                Console.WriteLine($"Number of listeners for test {count}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Redis Publish Exception: {ex.Message}");
                return false;
            }
        }
    }
}