namespace xptv.Models
{
    public class M3UChannel
    {
        public string TvgId { get; set; }
        public string TvgName { get; set; }
        public string TvgLogo { get; set; }
        public string GroupTitle { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return TvgName ?? Name;
        }
    }
}
