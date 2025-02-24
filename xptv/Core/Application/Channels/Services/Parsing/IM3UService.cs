using xptv.Core.Domain.Channels;

namespace xptv.Core.Application.Channels.Services.Parsing
{
    public interface IM3UService
    {
        Task<IEnumerable<Channel>> ReadM3UFileAsync(string filePath);
    }
}
