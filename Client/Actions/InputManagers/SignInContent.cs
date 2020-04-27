using System.Threading.Tasks;
using Common;

namespace Client.Actions.InputManagers
{
    public class SignInContent
    {
        public delegate Task<IPerson> ClientSignIn<out T>(string username, string password, string type);

        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public ClientSignIn<IPerson> SignInFunc { get; set; }
        public ActionBase MainMenuAction { get; set; }

        public SignInContent(string type, ClientSignIn<IPerson> signInFunc, ActionBase mainMenuAction)
        {
            Type = type;
            SignInFunc = signInFunc;
            MainMenuAction = mainMenuAction;
        }

        public async Task<IPerson> SignIn()
        {
            return await SignInFunc(Username, Password, Type);
        }
    }
}
