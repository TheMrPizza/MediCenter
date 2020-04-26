using System;
using Client.Exceptions;

namespace Client.IO.ConsoleIO
{
    public class ConsoleDateFieldElement : ConsoleFieldElement<DateTime>
    {
        protected override DateTime ValidateInput()
        {
            if (DateTime.TryParse(_input, out DateTime date))
            {
                return date;
            }

            throw new ParsingExcpetion("Field is not in date format");
        }
    }
}
