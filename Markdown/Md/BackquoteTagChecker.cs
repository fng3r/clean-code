namespace Markdown
{
    public class BackquoteTagChecker : MdTagChecker
    {
        public BackquoteTagChecker(string markdown) : base(markdown) { }
        
        public override bool IsOpeningTag(MdTag tag) => !IsTagEscaped(tag);

        public override bool IsClosingTag(MdTag tag) => !IsTagEscaped(tag);
    }
}