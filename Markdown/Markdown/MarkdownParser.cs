using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    public class MarkdownParser : IMarkdownParser
    {
        private readonly char[] escapeSymbols;
        private readonly char[] specialSymbols;
        private List<char> buffer;

        public MarkdownParser(char[] escapeSymbols, char[] specialSymbols)
        {
            this.escapeSymbols = escapeSymbols;
            this.specialSymbols = specialSymbols;
            buffer = new List<char>();
        }

        public string ParseLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                var currentSymbol = line[i];
                if (IsEscapeSymbol(currentSymbol))
                    ProcessEscapeSymbol(currentSymbol, line, ref i);
                else if (IsSpecialSymbol(currentSymbol))
                    ProcessSpecialSymbol(currentSymbol);
                else ProcessOrdinarSymbol(currentSymbol);
            }

            return string.Join("", buffer);
        }

        private bool IsEscapeSymbol(char currentSymbol) => escapeSymbols.Contains(currentSymbol);

        private bool IsSpecialSymbol(char symbol) => specialSymbols.Contains(symbol);

        private void ProcessEscapeSymbol(char symbol, string line, ref int currentIndex)
        {

        }

        private void ProcessOrdinarSymbol(char symbol)
        {

        }

        private void ProcessSpecialSymbol(char symbol)
        {

        }
    }
}