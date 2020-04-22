using System.Threading.Tasks;
using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class SignInAction : ActionBase
    {
        private OrderedDictionary _options;

        public SignInAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "A doctor", "doctors" }, {"A patient", "patients" } };
        }

        public async override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Sign in as...");
            string type = _streamIO.ListElement.Interact(_options) as string;
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            _client.User = await SignInAsync(username, password, type);
            if (_client.User == null)
            {
                return new HomeMenuAction(_client, _streamIO);
            }

            return new MainMenuAction(_client, _streamIO);
        }

        private async Task<IPerson> SignInAsync(string username, string password, string type)
        {
            try
            {
                if (type == "doctors")
                {
                    return await _client.SignInAsync<Doctor>(username, password, type);
                }

                return await _client.SignInAsync<Patient>(username, password, type);
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
