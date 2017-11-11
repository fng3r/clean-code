using System;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class Md
    {
        private readonly IMarkdownParser parser;

        public Md(IMarkdownParser parser)
        {
            this.parser = parser;
        }

        public string RenderToHtml(string markdown)
        {
            var lines = markdown.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var html = lines
                .Select(parser.ParseLine)
                .Aggregate(new StringBuilder(), (sb, line) => sb.AppendLine(line))
                .ToString();

            return new HtmlTag("p", html).ToString();
        }
    }
}