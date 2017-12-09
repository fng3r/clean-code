using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
    class TagFinder
    {
        private static readonly HashSet<string> mdTags = new HashSet<string> { "__", "_" };
        private readonly string markdown;

        public TagFinder(string markdown) => this.markdown = markdown;

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

                if (IsClosingTag(tag) && HasPairedOpeningTag(tag, openingTags))
                {
                    tag.IsOpening = false;
                    tags.Add(openingTags.Pop());
                    tags.Add(tag);
                }
                else if (IsOpeningTag(tag))
                    openingTags.Push(tag);
            }

            return tags.OrderBy(tag => tag.StartIndex).ToList();
        }

        private MdTag TryFindTag(int index)
            => mdTags
                .Where(key => markdown.SubstringMatch(index, key))
                .Select(key => new MdTag(index, key))
                .FirstOrDefault();

        private bool IsOpeningTag(MdTag tag) => !IsSpaceAfterTag(tag) && !IsTagEscaped(tag) && !IsDigitNearTag(tag);

        private bool IsClosingTag(MdTag tag) => !IsSpaceBeforeTag(tag) && !IsTagEscaped(tag) && !IsDigitNearTag(tag);

        private bool IsSpaceBeforeTag(MdTag tag) => tag.StartIndex > 0 && char.IsWhiteSpace(markdown[tag.StartIndex - 1]);

        private bool IsSpaceAfterTag(MdTag tag) => tag.EndIndex < markdown.Length && char.IsWhiteSpace(markdown[tag.EndIndex]);

        private bool IsTagEscaped(MdTag tag) => 
            tag.StartIndex > 0 && markdown[tag.StartIndex - 1] == '\\' &&
            (tag.StartIndex < 2 || markdown[tag.StartIndex - 2] != '\\');

        private bool IsDigitNearTag(MdTag tag) => 
            tag.StartIndex > 0 && char.IsDigit(markdown[tag.StartIndex - 1]) || 
            tag.EndIndex < markdown.Length && char.IsDigit(markdown[tag.EndIndex]);

        private bool HasPairedOpeningTag(MdTag closingTag, Stack<MdTag> openingTags) => openingTags.Count > 0 && openingTags.Peek().Value == closingTag.Value;
    }
}
