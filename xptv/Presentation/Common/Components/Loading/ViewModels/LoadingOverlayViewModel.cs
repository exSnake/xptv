using CommunityToolkit.Mvvm.ComponentModel;

namespace xptv.Presentation.Common.Components.Loading.ViewModels;

public partial class LoadingOverlayViewModel : ObservableObject, ILoadingOverlayViewModel
{
    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    [ObservableProperty]
    public partial string Message { get; set; } = "Initializing...";
}