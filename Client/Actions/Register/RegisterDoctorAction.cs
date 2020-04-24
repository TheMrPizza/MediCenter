using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class RegisterDoctorAction : ActionBase
    {
        public RegisterDoctorAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public async override Task<ActionBase> Run()
        {
            Doctor doctor = GetInput();
            await Register(doctor);
            return new HomeMenuAction(_client, _streamIO);
        }

        private async Task Register(Doctor doctor)
        {
            try
            {
                await _client.RegisterAsync(doctor, "doctors");
            }
            catch (MediCenterException e)
            {
                _streamIO.TextElement.Interact(e.Message);
            }
        }

        private Doctor GetInput()
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
