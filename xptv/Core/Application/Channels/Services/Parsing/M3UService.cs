using System.Text.RegularExpressions;
using xptv.Core.Domain.Channels;

namespace xptv.Core.Application.Channels.Services.Parsing
{
    public partial class M3UService : IM3UService
    {
        public async Task<IEnumerable<Channel>> ReadM3UFileAsync(string filePath)
        {
            var channels = new List<Channel>();
            string? extinf = null;

            await using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("#EXTINF:"))
                {
                    extinf = line;
                }
                else if (
                    !line.StartsWith('#')
                    && !string.IsNullOrWhiteSpace(line)
                    && extinf != null
                )
                {
                    var channel = ParseChannel(extinf, line);
                    channels.Add(channel);
                    extinf = null;
                }
            }

            return channels;
        }

        private Channel ParseChannel(string extinf, string url)
        {
            var channel = new Channel { Url = url };

            var tvgIdMatch = IdRegex().Match(extinf);
            var tvgNameMatch = NameRegex().Match(extinf);
            var tvgLogoMatch = LogoRegex().Match(extinf);
            var groupTitleMatch = GroupTitleRegex().Match(extinf);
            var nameMatch = GroupNameRegex().Match(extinf);

            channel.TvgId = tvgIdMatch.Success ? tvgIdMatch.Groups[1].Value : "";
            channel.TvgName = tvgNameMatch.Success ? tvgNameMatch.Groups[1].Value : "";
            channel.TvgLogo = tvgLogoMatch.Success ? tvgLogoMatch.Groups[1].Value : "";
            channel.GroupTitle = groupTitleMatch.Success ? groupTitleMatch.Groups[1].Value : "";
            channel.Name = nameMatch.Success ? nameMatch.Groups[1].Value.Trim() : "";

            return channel;
        }

        [GeneratedRegex(@"tvg-id=""([^""]*)""")]
        private static partial Regex IdRegex();

        [GeneratedRegex(@"tvg-name=""([^""]*)""")]
        private static partial Regex NameRegex();

        [GeneratedRegex(@"tvg-logo=""([^""]*)""")]
        private static partial Regex LogoRegex();

        [GeneratedRegex(@"group-title=""([^""]*)""")]
        private static partial Regex GroupTitleRegex();

        [GeneratedRegex(@"group-title=""[^""]*"",(.*)$")]
        private static partial Regex GroupNameRegex();
    }
}
