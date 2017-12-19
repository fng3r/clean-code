namespace Markdown
{
    public class MdTag
    {
        public MdTag(int startIndex, string value)
        {
            StartIndex = startIndex;
            Value = value;
            IsOpening = true;
        }

        public int StartIndex { get; }
        public int EndIndex => StartIndex + Value.Length;
        public string Value { get; }
        public bool IsOpening { get; set; }
    }
}