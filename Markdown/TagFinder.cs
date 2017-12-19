using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    public class TagFinder
    {
        private readonly Dictionary<string, IMdTagChecker> mdTagCheckers;
        private readonly IEnumerable<string> mdTags;
        private readonly string markdown;

        public TagFinder(string markdown)
        {
            this.markdown = markdown;
            mdTagCheckers = new Dictionary<string, IMdTagChecker>
            {
                ["__"] = new DoubleUnderscoreTagChecker(markdown),
                ["_"] = new SingleUnderscoreTagChecker(markdown),
                ["`"] = new BackquoteTagChecker(markdown)
            };
            mdTags = mdTagCheckers.Keys.OrderByDescending(t => t.Length);
        }

        public List<MdTag> FindTags()
        {
            var openingTags = new Stack<MdTag>();
            var tags = new List<MdTag>();
            var index = 0;
            while (index < markdown.Length)
            {
                var tag = TryFindTag(index);
                index += tag?.Value.Length ?? 1;
                if (tag == null) continue;

                var tagChecker = mdTagCheckers[tag.Value];
                if (tagChecker.IsClosingTag(tag) && HasPairedOpeningTag(tag, openingTags))
                {
                    tag.IsOpening = false;
                    tags.Add(openingTags.Pop());
                    tags.Add(tag);
                }
                else if (tagChecker.IsOpeningTag(tag))
                    openingTags.Push(tag);
            }

            return tags.OrderBy(tag => tag.StartIndex).ToList();
        }

        private MdTag TryFindTag(int index)
            => mdTags
                .Where(key => markdown.SubstringMatch(index, key))
                .Select(key => new MdTag(index, key))
                .FirstOrDefault();

        private static bool HasPairedOpeningTag(MdTag closingTag, Stack<MdTag> openingTags)
            => openingTags.Count > 0 && openingTags.Peek().Value == closingTag.Value;
    }
}
