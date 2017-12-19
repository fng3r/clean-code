namespace Markdown
{
    public class SingleUnderscoreTagChecker : MdTagChecker
    {
        public SingleUnderscoreTagChecker(string markdown) : base(markdown) { }
    }

    public class DoubleUnderscoreTagChecker : MdTagChecker
    {
        public DoubleUnderscoreTagChecker(string markdown) : base(markdown) { }
    }

    public class BackquoteTagChecker : MdTagChecker
    {
        public BackquoteTagChecker(string markdown) : base(markdown) { }
        
        public override bool IsOpeningTag(MdTag tag) => !IsTagEscaped(tag);

        public override bool IsClosingTag(MdTag tag) => !IsTagEscaped(tag);
    }

    public abstract class MdTagChecker : IMdTagChecker
    {
        protected readonly string markdown;

        protected MdTagChecker(string markdown)
        {
            this.markdown = markdown;
        }

        public virtual bool IsOpeningTag(MdTag tag) => !IsSpaceAfterTag(tag) && !IsTagEscaped(tag) && !IsDigitNearTag(tag);

        public virtual bool IsClosingTag(MdTag tag) => !IsSpaceBeforeTag(tag) && !IsTagEscaped(tag) && !IsDigitNearTag(tag);

        protected bool IsSpaceBeforeTag(MdTag tag) => tag.StartIndex > 0 && char.IsWhiteSpace(markdown[tag.StartIndex - 1]);

        protected bool IsSpaceAfterTag(MdTag tag) => tag.EndIndex < markdown.Length && char.IsWhiteSpace(markdown[tag.EndIndex]);

        protected bool IsTagEscaped(MdTag tag) =>
            tag.StartIndex > 0 && markdown[tag.StartIndex - 1] == '\\' &&
            (tag.StartIndex < 2 || markdown[tag.StartIndex - 2] != '\\');

        protected bool IsDigitNearTag(MdTag tag) =>
            tag.StartIndex > 0 && char.IsDigit(markdown[tag.StartIndex - 1]) ||
            tag.EndIndex < markdown.Length && char.IsDigit(markdown[tag.EndIndex]);
    }

    public interface IMdTagChecker
    {
        bool IsOpeningTag(MdTag tag);
        bool IsClosingTag(MdTag tag);
    }
}
