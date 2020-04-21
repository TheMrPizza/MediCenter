using System;
using System.Collections.Generic;
using System.Text;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class RegisterAction : ActionBase
    {
        public RegisterAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override ActionBase Run()
        {
            throw new NotImplementedException();
        }
    }
}
