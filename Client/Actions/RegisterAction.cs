using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions
{
    public class RegisterAction : ActionBase
    {
        private Dictionary<string, string> _options;

        public RegisterAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new Dictionary<string, string> {
                { "A doctor", "doctors" }, {"A patient", "patients" } };
        }

        public async override Task<ActionBase> Run()
        {
            string option = _streamIO.ListElement.Interact(new List<string>(_options.Keys));
            string name = _streamIO.FieldTextElement.Interact("Name");
            DateTime birthday = _streamIO.FieldDateElement.Interact("Birthday");
            string address = _streamIO.FieldTextElement.Interact("Address");
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            await _client.RegisterAsync(new Doctor(username, password, name, birthday, address), _options[option]);
            return new HomeMenuAction(_client, _streamIO);
        }
    }
}
