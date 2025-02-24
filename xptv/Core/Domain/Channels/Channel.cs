namespace xptv.Core.Domain.Channels
{
    public class Channel
    {
        public string TvgId { get; set; } = "NoID Provided";
        public string TvgName { get; set; } = "NoName Provided";
        public string TvgLogo { get; set; } = "NoLogo Provided";
        public string GroupTitle { get; set; } = "NoGroup Provided";
        public string Name { get; set; } = "NoName Provided";
        public string Url { get; set; } = "NoUrl Provided";

        public override string ToString()
        {
            // Assicurati che venga sempre mostrato un nome
            if (!string.IsNullOrEmpty(Name))
            {
                // Assicurati che venga sempre mostrato un nome
                return !string.IsNullOrEmpty(TvgName) ? TvgName : Name;
            }
            else
            {
                // Assicurati che venga sempre mostrato un nome
                return !string.IsNullOrEmpty(TvgName) ? TvgName : "Unnamed Channel";
            }
        }
    }
}
