using System;
using Client.Exceptions;

namespace Client.IO.ConsoleIO
{
    public class ConsoleBooleanFieldElement : ConsoleFieldElement<bool>
    {
        private const string TRUE_VALUE = "y";
        private const string FALSE_VALUE = "n";

        protected override void PrintFieldName()
        {
            Console.WriteLine($"{_fieldName} ({TRUE_VALUE}/{FALSE_VALUE}): ");
        }
        protected override bool ValidateInput()
        {
            if (_input != TRUE_VALUE && _input != FALSE_VALUE)
            {
                throw new ParsingExcpetion($"Field is not '{TRUE_VALUE}' or '{FALSE_VALUE}'");
            }

            return _input == TRUE_VALUE;
        }
    }
}
