using xptv.Presentation.Player.ViewModels;

namespace xptv.Presentation.Player.Views;

public partial class VideoPlayerPage : ContentPage
{
    public VideoPlayerPage(VideoPlayerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
