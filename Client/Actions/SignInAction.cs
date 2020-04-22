using System.Threading.Tasks;
using System.Collections.Generic;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class SignInAction : ActionBase
    {
        private Dictionary<string, string> _options;

        public SignInAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new Dictionary<string, string> {
                { "A doctor", "doctors" }, {"A patient", "patients" } };
        }

        public async override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Sign in as...");
            string option = _streamIO.ListElement.Interact(new List<string>(_options.Keys));
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            _client.User = await SignInAsync(username, password, option);
            if (_client.User == null)
            {
                return new HomeMenuAction(_client, _streamIO);
            }

            return new MainMenuAction(_client, _streamIO);
        }

        private async Task<IPerson> SignInAsync(string username, string password, string option)
        {
            try
            {
                if (option == "A doctor")
                {
                    return await _client.SignInAsync<Doctor>(username, password, _options[option]);
                }

                return await _client.SignInAsync<Patient>(username, password, _options[option]);
            }
            catch (NotFoundException e)
            {
                _streamIO.TextElement.Interact(e.Message);
            }
            catch (ConnectionException e)
            {
                _streamIO.TextElement.Interact(e.Message);
            }

            return null;
        }
    }
}
