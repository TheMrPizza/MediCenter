using Client.IO.Abstract;
using System;

namespace Client.IO.ConsoleIO
{
    public class ConsoleIO : IStreamIO
    {
        public TextElementBase TextElement { get; }
        public FieldElementBase<string> FieldTextElement { get; }
        public FieldElementBase<DateTime> FieldDateElement { get; }
        public ListElementBase ListElement { get; }

        public ConsoleIO()
        {
            TextElement = new ConsoleTextElement();
            FieldTextElement = new ConsoleTextFieldElement();
            FieldDateElement = new ConsoleDateFieldElement();
            ListElement = new ConsoleListElement();
        }
    }
}
