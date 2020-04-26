using System;
using Client.Exceptions;

namespace Client.IO.ConsoleIO
{
    public class ConsoleBooleanFieldElement : ConsoleFieldElement<bool>
    {
        protected override void PrintFieldName()
        {
            Console.WriteLine(_fieldName + " (y/n): ");
        }
        protected override bool ValidateInput()
        {
            if (_input != "y" && _input != "n")
            {
                throw new ParsingExcpetion("Field is not 'y' or 'n'");
            }

            return _input == "y";
        }
    }
}
