using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Client.Exceptions;

namespace Client.IO.Abstract
{
    public abstract class ListElementBase
    {
        protected List<ListOption> _options { get; set; }
        protected string _input { get; set; }

        public object Interact(List<ListOption> options)
        {
            _options = options;
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

        public object Interact(OrderedDictionary optionDict)
        {
            var options = new List<ListOption>();
            foreach (DictionaryEntry entry in optionDict)
            {
                options.Add(new ListOption(entry.Key as string, entry.Value));
            }

            return Interact(options);
        }

        public object Interact(List<string> names)
        {
            var options = names.Select(name => new ListOption(name)).ToList();
            return Interact(options);
        }

        private object Execute()
        {
            PrintOptions();
            ReadInput();
            return ValidateInput();
        }

        protected abstract void PrintOptions();
        protected abstract object ValidateInput();
        protected abstract void ReadInput();
        protected abstract void PrintException(MediCenterException exception);
    }
}
