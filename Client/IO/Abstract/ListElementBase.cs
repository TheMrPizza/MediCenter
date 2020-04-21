using System.Collections.Generic;
using Client.Exceptions;

namespace Client.IO.Abstract
{
    public abstract class ListElementBase
    {
        protected List<string> _options { get; set; }
        protected string _input { get; set; }

        public string Interact(List<string> options)
        {
            _options = options;
            while (true)
            {
                try
                {
                    return Execute();
                }
                catch (MediCenterException e)
                {
                    PrintException(e);
                }
            }
        }

        private string Execute()
        {
            PrintOptions();
            ReadInput();
            return ValidateInput();
        }

        protected abstract void PrintOptions();
        protected abstract string ValidateInput();
        protected abstract string ReadInput();
        protected abstract void PrintException(MediCenterException exception);
    }
}
