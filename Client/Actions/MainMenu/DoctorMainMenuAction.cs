using System.Threading.Tasks;
using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions
{
    public class DoctorMainMenuAction : ActionBase
    {
        private OrderedDictionary _options;

        public DoctorMainMenuAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "View my visits",  new ViewVisitsAction(client, streamIO) } };
        }

        public override Task<ActionBase> Run()
        {
            _streamIO.TextElement.Interact($"Hello Dr. {_client.User.Name}!");
            _streamIO.TextElement.Interact("What would you like to do?");
            ActionBase nextAction = _streamIO.ListElement.Interact(_options) as ActionBase;
            return Task.FromResult(nextAction);
        }
    }
}
