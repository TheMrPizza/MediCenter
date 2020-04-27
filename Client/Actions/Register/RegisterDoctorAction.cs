using System.Threading.Tasks;
using Client.Actions.IOManagers;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class RegisterDoctorAction : ActionBase
    {
        public IOManagerBase<Doctor> IOManager { get; set; }

        public RegisterDoctorAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            IOManager = new RegisterDoctorIO(client, streamIO);
        }

        public async override Task<ActionBase> Run()
        {
            Doctor doctor = IOManager.GetInput();
            await Register(doctor);
            return new HomeMenuAction(_client, _streamIO);
        }

        private async Task Register(Doctor doctor)
        {
            try
            {
                await _client.Register(doctor, "doctors");
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
            }
        }
    }
}
