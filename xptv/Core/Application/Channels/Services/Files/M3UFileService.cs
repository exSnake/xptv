namespace xptv.Core.Application.Channels.Services.Files
{
    public class M3UFileService : IM3UFileService
    {
        private const string lastM3UPathKey = "LastM3UPath";

        public async Task<string?> PickFileAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(
                    new PickOptions
                    {
                        FileTypes = new FilePickerFileType(
                            new Dictionary<DevicePlatform, IEnumerable<string>>
                            {
                                    { DevicePlatform.WinUI, [".m3u", ".m3u8"] },
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

        public async Task<string?> GetLastPathAsync()
        {
            var path = Preferences.Get(lastM3UPathKey, string.Empty);
            return !string.IsNullOrEmpty(path) && File.Exists(path) ? path : null;
        }

        public Task SaveLastPathAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}
