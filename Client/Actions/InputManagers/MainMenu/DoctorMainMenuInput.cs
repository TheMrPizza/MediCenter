using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;

namespace Client.Actions.InputManagers
{
    public class DoctorMainMenuInput : InputManagerBase<ActionBase>
    {
        private OrderedDictionary _options;

        public DoctorMainMenuInput(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = new OrderedDictionary {
                { "View my visits",  new ViewVisitsAction(client, streamIO) },
                { "Give prescription", new GivePrescriptionsAction(client, streamIO) } };
        }

        public override void PrintInstructions()
        {
            _streamIO.TextElement.Interact($"Hello Dr. {_client.User.Name}!");
            _streamIO.TextElement.Interact("What would you like to do?");
        }

        public override ActionBase GetInput()
        {
            return _streamIO.ListElement.Interact(_options) as ActionBase;
        }
    }
}
