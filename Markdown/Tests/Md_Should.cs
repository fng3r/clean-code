using FluentAssertions;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class Md_Should
    {
        [Test]
        public void RenderPlainText()
        {
            Md.RenderToHtml("Some text")
                .Should().Be("Some text");
        }

        [Test]
        public void RenderPlainText_WithMultipleSpaces()
        {
            Md.RenderToHtml("Some       spaces")
                .Should().Be("Some       spaces");
        }

        [Test]
        public void RenderItalic_WithSingleWordInside()
        {
            var result = Md.RenderToHtml("_abc_");
            result.Should().Be("<em>abc</em>");
        }

        [Test]
        public void RenderBold_WithSingleWordInside()
        {
            Md.RenderToHtml("__single__")
                .Should().Be("<strong>single</strong>");
        }

        [Test]
        public void RenderBoldAndItalic_WithSpacesInside()
        {
            Md.RenderToHtml("  __Some   text__ with _some  spaces_  ")
                .Should().Be("  <strong>Some   text</strong> with <em>some  spaces</em>  ");
        }

        [Test]
        public void RenderItalic_WhenInsideBold()
        {
           Md.RenderToHtml("aaa b __bold _italic_ bold__")
                .Should().Be("aaa b <strong>bold <em>italic</em> bold</strong>");
        }

        [Test]
        public void RenderBold_WhenInsideItalic()
        {
            Md.RenderToHtml("_a __b__ c_")
                .Should().Be("<em>a <strong>b</strong> c</em>");
        }

        [TestCase("_ abc_", TestName = "after opening tag")]
        [TestCase("_abc _", TestName = "before closing tag")]
        public void IgnoreTags_IfTheyAreSpaced(string markdown)
        {
            Md.RenderToHtml(markdown)
                .Should().Be(markdown);
        }

        [TestCase("_12_2", TestName = "digit after closing tag")]
        [TestCase("1_abc_", TestName = "digit before opening tag")]
        public void IgnoreTags_WithDigits(string markdown)
        {
            Md.RenderToHtml(markdown)
                .Should().Be(markdown);
        }

        [Test]
        public void IgnoreTags_IfEscaped()
        {
             Md.RenderToHtml(@"\_a_")
                .Should().Be(@"_a_");
        }

        [Test]
        public void RenderText_WithEscapeSymbol_EscapedByItself()
        {
            Md.RenderToHtml(@"\\\\_a\\_")
                .Should().Be(@"\\<em>a\</em>");
        }
    }
}