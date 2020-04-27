using System.Threading.Tasks;
using Client.Actions.InputManagers;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class SignInAction : ActionBase
    {
        public InputManagerBase<SignInContent> InputManager { get; set; }

        public SignInAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            InputManager = new SignInInput(client, streamIO);
        }

        public async override Task<ActionBase> Run()
        {
            InputManager.PrintInstructions();
            SignInContent content = InputManager.GetInput();
            if (await SignIn(content))
            {
                return MainMenuAction;
            }

            return new HomeMenuAction(_client, _streamIO);
        }

        private async Task<bool> SignIn(SignInContent content)
        {
            try
            {
                _client.User = await content.SignIn();
                MainMenuAction = content.MainMenuAction;
                return true;
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return false;
            }
        }
    }
}
