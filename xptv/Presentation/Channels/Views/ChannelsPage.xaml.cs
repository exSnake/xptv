using xptv.Presentation.Channels.ViewModels;

namespace xptv
{
    public partial class ChannelsPage : ContentPage
    {
        public ChannelsPage(ChannelsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            Loaded += async (s, e) => await viewModel.InitializeAsync();
        }
    }
}
