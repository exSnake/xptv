namespace xptv.Core.Application.Channels.Services.Files
{
    public interface IM3UFileService
    {
        public Task<string?> PickFileAsync();
        public Task<string?> GetLastPathAsync();
        public Task SaveLastPathAsync(string path);
    }
}
