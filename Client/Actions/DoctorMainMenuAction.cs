using System;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class DoctorMainMenuAction : ActionBase
    {
        private OrderedDictionary _options;

        public DoctorMainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Task<ActionBase> Run()
        {
            throw new NotImplementedException();
        }
    }
}
