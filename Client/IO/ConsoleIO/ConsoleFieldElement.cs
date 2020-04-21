using System;
using Client.IO.Abstract;

namespace Client.IO.ConsoleIO
{
    public class ConsoleFieldElement : FieldElementBase
    {
        protected override void PrintFieldName()
        {
            Console.Write(_fieldName + ": ");
        }

        protected override string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
