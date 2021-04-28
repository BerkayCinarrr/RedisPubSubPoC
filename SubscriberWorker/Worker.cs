using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CommonRedis;

namespace SubscriberWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {                                
                    var sub = RedisStore.RedisCache.Multiplexer.GetSubscriber();       
                    await sub.SubscribeAsync("testChannel", (channel, message) =>  //new RedisChannel("b*c", RedisChannel.PatternMode.Pattern)
                    {
                        Console.WriteLine("Got notification: " + (string)message);
                        Console.ReadKey();
                        //do stuff
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Redis Publish Exception: {ex.Message}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
