using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
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
            string username = _streamIO.FieldElement.Interact("Username");
            string password = _streamIO.FieldElement.Interact("Password");
            _client.User = await _client.SignInAsync(username, password);
            return new MainMenuAction(_client, _streamIO);
        }
    }
}
