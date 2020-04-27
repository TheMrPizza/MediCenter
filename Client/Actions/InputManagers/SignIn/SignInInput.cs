using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions.InputManagers
{
    public class SignInInput : InputManagerBase<SignInContent>
    {
        private OrderedDictionary _options;

        public SignInInput(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "A doctor", new SignInContent("doctors", client.SignIn<Doctor>,
                                                new DoctorMainMenuAction(client, streamIO)) },
                { "A patient", new SignInContent("patients", client.SignIn<Patient>,
                                                 new PatientMainMenuAction(client, streamIO)) } };
        }

        public override SignInContent GetInput()
        {
            _streamIO.TextElement.Interact("Sign in as...");
            SignInContent signIn = _streamIO.ListElement.Interact(_options) as SignInContent;
            signIn.Username = _streamIO.FieldTextElement.Interact("Username");
            signIn.Password = _streamIO.FieldTextElement.Interact("Password");
            return signIn;
        }
    }
}
