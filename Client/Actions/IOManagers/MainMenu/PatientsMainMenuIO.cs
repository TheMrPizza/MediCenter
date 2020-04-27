using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions.IOManagers
{
    public class PatientsMainMenuIO : IOManagerBase<ActionBase>
    {
        private OrderedDictionary _options { get; set; }

        public PatientsMainMenuIO(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "Order a visit",  new OrderVisitAction(client, streamIO) },
                { "View my visits", new ViewVisitsAction(client, streamIO) } };
        }

        public override void PrintInstructions()
        {
            _streamIO.TextElement.Interact($"Hello {_client.User.Name}!");
            _streamIO.TextElement.Interact("What would you like to do?");
        }

        public override ActionBase GetInput()
        {
            return _streamIO.ListElement.Interact(_options) as ActionBase;
        }
    }
}
