using CommunityToolkit.Mvvm.ComponentModel;
using xptv.Core.Domain.Channels;

namespace xptv.Presentation.Player.ViewModels;

[QueryProperty(nameof(Channel), nameof(Channel))]
public partial class VideoPlayerViewModel : ObservableObject
{
    [ObservableProperty]
    private Channel _channel;
}
