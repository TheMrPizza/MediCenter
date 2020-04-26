using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class ViewVisitsAction : ActionBase
    {
        public ViewVisitsAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Task<ActionBase> Run()
        {
            throw new NotImplementedException();
        }
    }
}
