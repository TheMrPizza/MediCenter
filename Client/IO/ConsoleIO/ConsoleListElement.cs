using System;
using Client.IO.Abstract;
using Client.Exceptions;

namespace Client.IO.ConsoleIO
{
    public class ConsoleListElement : ListElementBase
    {
        protected override void PrintOptions()
        {
            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + _options[i]);
            }
        }

        protected override string ValidateInput()
        {
            if (!int.TryParse(_input, out _))
            {
                throw new ParsingExcpetion("Input is not an integer");
            }

            int inputNum = int.Parse(_input);
            if (inputNum < 1 || inputNum > _options.Count)
            {
                throw new ParsingExcpetion("Option number is out of range");
            }

            return _options[inputNum - 1];
        }

        protected override string ReadInput()
        {
            return Console.ReadLine();
        }

        protected override void PrintException(MediCenterException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
