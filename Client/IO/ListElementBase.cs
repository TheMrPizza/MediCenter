using System;
using System.Collections.Generic;
using System.Text;

namespace Client.IO
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
                catch (FormatException e)
                {
                    PrintException(e);
                }
            }
        }

        private string Execute()
        {
            PrintOptions();
            ReadInput();
            ValidateInput();
            return _input;
        }

        protected abstract void PrintOptions();
        protected abstract string ReadInput();
        protected abstract bool ValidateInput();
        protected abstract void PrintException(Exception exception);
    }
}
