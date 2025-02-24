using xptv.Core.Domain.Channels;

namespace xptv.Core.Application.Channels.Services.Groups
{
    public interface IChannelGroupService
    {
        public List<string> GetGroups(IEnumerable<Channel> channels);
        public IEnumerable<Channel> FilterChannelsByGroup(IEnumerable<Channel> channels, string group);
    }
}
