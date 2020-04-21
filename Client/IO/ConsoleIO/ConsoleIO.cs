using Client.IO.Abstract;

namespace Client.IO.ConsoleIO
{
    public class ConsoleIO : IStreamIO
    {
        public TextElementBase TextElement { get; }
        public FieldElementBase FieldElement { get; }
        public ListElementBase ListElement { get; }

        public ConsoleIO()
        {
            TextElement = new ConsoleTextElement();
            FieldElement = new ConsoleFieldElement();
            ListElement = new ConsoleListElement();
        }
    }
}
