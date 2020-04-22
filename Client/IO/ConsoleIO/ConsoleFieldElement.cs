using System;
using Client.IO.Abstract;
using Client.Exceptions;

namespace Client.IO.ConsoleIO
{
    public abstract class ConsoleFieldElement<T> : FieldElementBase<T>
    {
        protected override void PrintFieldName()
        {
            Console.Write(_fieldName + ": ");
        }

        protected override void ReadInput()
        {
            _input = Console.ReadLine();
        }

        protected override void PrintException(MediCenterException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
