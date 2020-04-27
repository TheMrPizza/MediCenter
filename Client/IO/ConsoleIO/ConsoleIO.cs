using System;
using Client.IO.Abstract;

namespace Client.IO.ConsoleIO
{
    public class ConsoleIO : IStreamIO
    {
        public TextElementBase TextElement { get; }
        public FieldElementBase<string> FieldTextElement { get; }
        public FieldElementBase<bool> FieldBooleanElement { get; }
        public FieldElementBase<DateTime> FieldDateElement { get; }
        public ListElementBase ListElement { get; }
        public ErrorElementBase ErrorElement { get; }

        public ConsoleIO()
        {
            TextElement = new ConsoleTextElement();
            FieldTextElement = new ConsoleTextFieldElement();
            FieldBooleanElement = new ConsoleBooleanFieldElement();
            FieldDateElement = new ConsoleDateFieldElement();
            ListElement = new ConsoleListElement();
            ErrorElement = new ConsoleErrorElement();
        }
    }
}
