using System;
using System.Collections.Generic;
using System.Text;
using Client.HttpClients;
using Client.IO;

namespace Client.Actions
{
    public abstract class ActionBase
    {
        private HttpClient _client { get; }
        private IStreamIO _streamIO { get; }

        public void Run()
        {

        }

        public abstract void PrintInstructions();
    }
}
