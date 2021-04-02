namespace GMWU
{
    public class AddonJson
    {
        public string title { get; set; }
        public string type { get; set; }
        public string[] tags { get; set; }
        public string[] ignore { get; set; }

        public AddonJson(string title, string type, string[] tags, string[] ignore)
        {
            this.title = title;
            this.type = type;
            this.tags = tags;
            this.ignore = ignore;
        }
    }
}
