using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class Md
    {
        private static readonly Dictionary<string, HtmlTag> mdToHtml = new Dictionary<string, HtmlTag>
        {
            ["_"] = new HtmlTag("em"),
            ["__"] = new HtmlTag("strong")
        };

        public static string RenderToHtml(string markdown)
        {
            var tagFinder = new TagFinder(markdown);
            var tags = tagFinder.FindTags();
            return MdToHtml(markdown, tags);
        }

        private static string MdToHtml(string markdown, List<MdTag> tags)
        {
            var result = new StringBuilder();
            var index = 0;
            foreach (var tag in tags)
            {
                result.Append(markdown.UnescapeSubstring(index, tag.StartIndex - index));
                var htmlTag = mdToHtml[tag.Value];
                result.Append(tag.IsOpening ? htmlTag.OpeningTag : htmlTag.ClosingTag);
                index = tag.EndIndex;
            }

            result.Append(markdown.Substring(index).UnescapeSubstring());
            return result.ToString();
        }
    }
}