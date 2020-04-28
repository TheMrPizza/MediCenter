using System;
using Client.Exceptions;
using Client.IO.Abstract;

namespace Client.IO.ConsoleIO
{
    public class ConsoleErrorElement : ErrorElementBase
    {
        public override void Interact(MediCenterException exception)
        {
            Interact(exception.Message);
        }

        public override void Interact(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
