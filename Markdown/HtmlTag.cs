namespace Markdown
{
    public class HtmlTag
    {
        public HtmlTag(string tagName)
        {
            OpeningTag = $"<{tagName}>";
            ClosingTag = $"</{tagName}>";
        }

        public string OpeningTag { get; }
        public string ClosingTag { get; }
    }
}