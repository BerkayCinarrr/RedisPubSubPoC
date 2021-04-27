using StackExchange.Redis;
using System;
using System.Net;

namespace CommonRedis
{
public class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {
            var configurationOptions = new ConfigurationOptions
            {
                 EndPoints = {
                    new DnsEndPoint("redis", 6379),
                    //new IPEndPoint(host, port), // <== or this if 'host' is an IPAddress
                },
                //ResolveDns = true,
                AbortOnConnectFail = false,
                //KeepAlive = 180,
                //ConnectTimeout = 10000,
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }
}