using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions
{
    public abstract class ActionBase
    {
        protected MediClient _client { get; }
        protected IStreamIO _streamIO { get; }
        protected ActionBase MainMenuAction { get; set; }

        public ActionBase(MediClient client, IStreamIO streamIO)
        {
            _client = client;
            _streamIO = streamIO;
        }

        public Task<ActionBase> Act()
        {

        }

        public void SetClientUser(Doctor doctor)
        {
            MainMenuAction = new DoctorMainMenuAction(_client, _streamIO);
            _client.User = doctor;
        }

        public void SetClientUser(Patient patient)
        {
            MainMenuAction = new PatientMainMenuAction(_client, _streamIO);
            _client.User = patient;
        }

        public abstract Task<ActionBase> Run();
    }
}
