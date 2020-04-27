using System;
using System.Collections.Generic;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions.InputManagers
{
    public class RegisterDoctorInput : InputManagerBase<Doctor>
    {
        public RegisterDoctorInput(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public override Doctor GetInput()
        {
            string username = _streamIO.FieldTextElement.Interact("Username");
            string password = _streamIO.FieldTextElement.Interact("Password");
            string name = _streamIO.FieldTextElement.Interact("Name");
            DateTime birthday = _streamIO.FieldDateElement.Interact("Birthday");
            string address = _streamIO.FieldTextElement.Interact("Address");
            List<Speciality> specialities = GetSpecialities();

            return new Doctor(username, password, name, birthday, address, specialities);
        }

        private List<Speciality> GetSpecialities()
        {
            var specialities = new List<Speciality>();
            var allSpecialities = Enum.GetValues(typeof(Speciality));

            _streamIO.TextElement.Interact("Enter your specialities:");
            foreach (Speciality s in allSpecialities)
            {
                if (_streamIO.FieldBooleanElement.Interact(s.ToString()))
                {
                    specialities.Add(s);
                }
            }

            return specialities;
        }
    }
}
