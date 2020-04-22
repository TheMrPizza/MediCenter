using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions
{
    public class RegisterAction : ActionBase
    {
        private OrderedDictionary _options;

        public RegisterAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "A doctor", "doctors" }, {"A patient", "patients" } };
        }

        public async override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Register as...");
            string type = _streamIO.ListElement.Interact(_options) as string;
            if (type == "doctors")
            {
                await _client.RegisterAsync(RegisterDoctor(), type);
            }
            else
            {
                await _client.RegisterAsync(RegisterPatient(), type);
            }
            
            return new HomeMenuAction(_client, _streamIO);
        }

        public Doctor RegisterDoctor()
        {
            string name = _streamIO.FieldTextElement.Interact("Name");
            DateTime birthday = _streamIO.FieldDateElement.Interact("Birthday");
            string address = _streamIO.FieldTextElement.Interact("Address");
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            return new Doctor(username, password, name, birthday, address);
        }

        public Patient RegisterPatient()
        {
            string name = _streamIO.FieldTextElement.Interact("Name");
            DateTime birthday = _streamIO.FieldDateElement.Interact("Birthday");
            string address = _streamIO.FieldTextElement.Interact("Address");
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            return new Patient(username, password, name, birthday, address);
        }
    }
}
