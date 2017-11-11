namespace Markdown
{
    public class HtmlTag
    {
        public HtmlTag(string tagName, string content)
        {
            TagName = tagName;
            Content = content;
        }

        public string TagName { get; }
        public string Content { get; }

        public override string ToString()
        {
            return $"<{TagName}>{Content}</{TagName}>";
        }
    }
}