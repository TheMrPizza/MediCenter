using System;
using System.Collections.Generic;
using System.Text;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class HomeMenuAction : ActionBase
    {
        private Dictionary<string, ActionBase> _options;

        public HomeMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new Dictionary<string, ActionBase> {
                { "Sign In", new SignInAction(client, streamIO) },
                { "Register", new RegisterAction(client, streamIO) } };
        }

        public override ActionBase Run()
        {
            _streamIO.TextElement.Interact("Welcome to MediCenter!");
            string option = _streamIO.ListElement.Interact(new List<string>(_options.Keys));
            return _options[option];
        }
    }
}
