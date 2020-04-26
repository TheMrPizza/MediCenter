using System;
using Client.Exceptions;
using Client.IO.Abstract;

namespace Client.IO.ConsoleIO
{
    public class ConsoleErrorElement : ErrorElementBase
    {
        public override void Interact(MediCenterException exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception.Message);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
