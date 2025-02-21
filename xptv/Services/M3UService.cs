using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xptv.Models;

namespace xptv.Services
{
    public class M3UService : IM3UService
    {
        public async Task<IEnumerable<M3UChannel>> ReadM3UFileAsync(string filePath)
        {
            var channels = new List<M3UChannel>();
            string extinf = null;

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.StartsWith("#EXTINF:"))
                    {
                        extinf = line;
                    }
                    else if (!line.StartsWith("#") && !string.IsNullOrWhiteSpace(line) && extinf != null)
                    {
                        var channel = ParseChannel(extinf, line);
                        channels.Add(channel);
                        extinf = null;
                    }
                }
            }
            return channels;
        }

        private M3UChannel ParseChannel(string extinf, string url)
        {
            var channel = new M3UChannel { Url = url };

            var tvgIdMatch = Regex.Match(extinf, @"tvg-id=""([^""]*)""");
            var tvgNameMatch = Regex.Match(extinf, @"tvg-name=""([^""]*)""");
            var tvgLogoMatch = Regex.Match(extinf, @"tvg-logo=""([^""]*)""");
            var groupTitleMatch = Regex.Match(extinf, @"group-title=""([^""]*)""");
            var nameMatch = Regex.Match(extinf, @"group-title=""[^""]*"",(.*)$");

            channel.TvgId = tvgIdMatch.Success ? tvgIdMatch.Groups[1].Value : "";
            channel.TvgName = tvgNameMatch.Success ? tvgNameMatch.Groups[1].Value : "";
            channel.TvgLogo = tvgLogoMatch.Success ? tvgLogoMatch.Groups[1].Value : "";
            channel.GroupTitle = groupTitleMatch.Success ? groupTitleMatch.Groups[1].Value : "";
            channel.Name = nameMatch.Success ? nameMatch.Groups[1].Value.Trim() : "";

            return channel;
        }
    }
}
