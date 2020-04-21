using System;
using System.Collections.Generic;
using System.Text;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class SignInAction : ActionBase
    {
        private List<string> _types;

        public SignInAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _types = new List<string> { "Doctors", "Patients" };
        }

        public override ActionBase Run()
        {
            _streamIO.ListElement.Interact(_types);
        }
    }
}
