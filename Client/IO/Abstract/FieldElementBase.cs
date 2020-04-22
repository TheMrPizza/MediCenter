using Client.Exceptions;

namespace Client.IO.Abstract
{
    public abstract class FieldElementBase<T>
    {
        protected string _fieldName { get; set; }
        protected string _input { get; set; }

        public T Interact(string fieldName)
        {
            _fieldName = fieldName;
            while (true)
            {
                try
                {
                    return Execute();
                }
                catch (ParsingExcpetion e)
                {
                    PrintException(e);
                }
            }
        }

        private T Execute()
        {
            PrintFieldName();
            ReadInput();
            return ValidateInput();
        }

        protected abstract void PrintFieldName();
        protected abstract T ValidateInput();
        protected abstract void ReadInput();
        protected abstract void PrintException(MediCenterException exception);
    }
}
