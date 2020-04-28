using System;
using System.Collections.Generic;
using Client.IO.Abstract;
using Client.HttpClients;
using Common;

namespace Client.Actions.InputManagers
{
    public class RegisterPatientInput : InputManagerBase<Patient>
    {
        public RegisterPatientInput(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Patient GetInput()
        {
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            string name = _streamIO.FieldTextElement.Interact("Name");
            DateTime birthday = _streamIO.FieldDateElement.Interact("Birthday");
            string address = _streamIO.FieldTextElement.Interact("Address");
            List<Disease> diseases = GetDiseases();

            return new Patient(username, password, name, birthday, address, diseases);
        }

        private List<Disease> GetDiseases()
        {
            var diseases = new List<Disease>();
            var allDiseases = Enum.GetValues(typeof(Disease));

            _streamIO.TextElement.Interact("Enter your diseases:");
            foreach (Disease d in allDiseases)
            {
                if (_streamIO.FieldBooleanElement.Interact(d.ToString()))
                {
                    diseases.Add(d);
                }
            }

            return diseases;
        }
    }
}
