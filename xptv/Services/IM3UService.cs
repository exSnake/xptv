using xptv.Models;

namespace xptv.Services
{
    public interface IM3UService
    {
        Task<IEnumerable<M3UChannel>> ReadM3UFileAsync(string filePath);
    }
}
