using System.Collections.Specialized;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class RegisterAction : ActionBase
    {
        private OrderedDictionary _options;

        public RegisterAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "A doctor", new RegisterDoctorAction(client, streamIO) },
                { "A patient", new RegisterPatientAction(client, streamIO) } };
        }

        public override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact("Register as...");
            ActionBase nextAction = _streamIO.ListElement.Interact(_options) as ActionBase;
            return Task.FromResult(nextAction);
        }
    }
}
