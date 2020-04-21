using System;
using Client.IO.Abstract;

namespace Client.IO.ConsoleIO
{
    public class ConsoleTextElement : TextElementBase
    {
        protected override void PrintText()
        {
            Console.WriteLine(_text);
            Console.WriteLine();
        }
    }
}
