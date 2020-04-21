using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public abstract class ActionBase
    {
        protected MediClient _client { get; }
        protected IStreamIO _streamIO { get; }

        public ActionBase(MediClient client, IStreamIO streamIO)
        {
            _client = client;
            _streamIO = streamIO;
        }

        public abstract Task<ActionBase> Run();
    }
}
