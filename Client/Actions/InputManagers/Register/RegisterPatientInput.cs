using System;
using Client.IO.Abstract;
using Client.HttpClients;
using Common;

namespace Client.Actions.InputManagers
{
    public class RegisterPatientIO : InputManagerBase<Patient>
    {
        public RegisterPatientIO(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Patient GetInput()
        {
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            string name = _streamIO.FieldTextElement.Interact("Name");
            DateTime birthday = _streamIO.FieldDateElement.Interact("Birthday");
            string address = _streamIO.FieldTextElement.Interact("Address");

            return new Patient(username, password, name, birthday, address);
        }
    }
}
