namespace Markdown
{
    public class HtmlTag
    {
        public HtmlTag(string tagName)
        {
            OpeningTag = $"<{tagName}>";
            ClosingTag = $"</{tagName}>";
        }

        public string OpeningTag { get; set; }
        public string ClosingTag { get; set; }
    }
}