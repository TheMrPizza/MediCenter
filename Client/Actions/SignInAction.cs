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
        private List<string> _options;

        public SignInAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new List<string> { "A doctor", "A patient" };
        }

        public async override Task<ActionBase> Run()
        {
            string option = _streamIO.ListElement.Interact(_options);
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            _client.User = await SignInAsync(username, password, option);
            return new MainMenuAction(_client, _streamIO);
        }

        private async Task<IPerson> SignInAsync(string username, string password, string option)
        {
            try
            {
                if (option == "A doctor")
                {
                    return await _client.SignInAsync<Doctor>(username, password);
                }

                return await _client.SignInAsync<Patient>(username, password);
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
