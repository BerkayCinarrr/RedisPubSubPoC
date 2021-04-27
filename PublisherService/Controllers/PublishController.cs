using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublisherService.Services.Interfaces;
using PublisherService.Models;

namespace PublisherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly ILogger<PublishController> _logger;
        private readonly IRedisPublisher _redisPublisher;
        public PublishController(ILogger<PublishController> logger, IRedisPublisher redisPublisher)
        {
            _logger = logger;
            _redisPublisher = redisPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> Publish([FromBody] RedisPublish data)
        {
            return Ok(await _redisPublisher.Publish(data.msg));
        }

    }
}