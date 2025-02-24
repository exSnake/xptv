using xptv.Presentation.Player.Views;

namespace xptv
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VideoPlayerPage), typeof(VideoPlayerPage));
        }
    }
}
