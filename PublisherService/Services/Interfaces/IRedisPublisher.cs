using System.Threading.Tasks;

namespace PublisherService.Services.Interfaces
{
    public interface IRedisPublisher 
    {
        Task<bool> Publish(string msg);
    }
}