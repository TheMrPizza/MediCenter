using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class SignInAction : ActionBase
    {
        public SignInAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            
        }

        public async override Task<ActionBase> Run()
        {
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            _client.User = await SignInAsync(username, password);
            return new MainMenuAction(_client, _streamIO);
        }

        private async Task<IPerson> SignInAsync(string username, string password)
        {
            try
            {
                return await _client.SignInAsync(username, password);
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
