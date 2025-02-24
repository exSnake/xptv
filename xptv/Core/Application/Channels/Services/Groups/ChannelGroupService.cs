using xptv.Core.Domain.Channels;

namespace xptv.Core.Application.Channels.Services.Groups;

public class ChannelGroupService : IChannelGroupService
{
    public List<string> GetGroups(IEnumerable<Channel> channels)
    {
        return channels
            .Select(c => c.GroupTitle)
            .Where(g => !string.IsNullOrEmpty(g))
            .Distinct()
            .Order()
            .Prepend("All")
            .ToList();
    }

    public IEnumerable<Channel> FilterChannelsByGroup(IEnumerable<Channel> channels, string group)
    {
        return group == "All" 
            ? channels 
            : channels.Where(c => c.GroupTitle == group);
    }
}