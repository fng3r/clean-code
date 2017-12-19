namespace Markdown
{
    public interface IMdTagChecker
    {
        bool IsOpeningTag(MdTag tag);
        bool IsClosingTag(MdTag tag);
    }
}