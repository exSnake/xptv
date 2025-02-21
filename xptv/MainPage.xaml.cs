using xptv.Services;

namespace xptv
{
    public partial class MainPage : ContentPage
    {
        private readonly IM3UService _m3uService;
        public MainPage(IM3UService m3uService)
        {
            InitializeComponent();
            _m3uService = m3uService;
        }

        private async void OnOpenM3UFileClicked(object sender, EventArgs e)
        {
            var filePath = await PickM3UFileAsync();
            if (filePath != null)
            {
                var lines = await _m3uService.ReadM3UFileAsync(filePath);
                M3UListView.ItemsSource = lines;
            }
        }

        private async Task<string> PickM3UFileAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(
                    new PickOptions
                    {
                        FileTypes = new FilePickerFileType(
                            new Dictionary<DevicePlatform, IEnumerable<string>>
                            {
                                { DevicePlatform.WinUI, new[] { ".m3u", ".m3u8" } },
                            }
                        ),
                    }
                );

                return result?.FullPath;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                string url = e.SelectedItem.ToString();
                // Qui in futuro aggiungeremo la logica per riprodurre l'URL selezionato
                DisplayAlert("Selected Stream", url, "OK");

                // Deseleziona l'item
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
