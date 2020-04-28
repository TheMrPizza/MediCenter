using System.Collections.Generic;
using System.Collections.Specialized;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Actions.OutputManagers;
using Common;

namespace Client.Actions.InputManagers
{
    public class GivePrescriptionsInput : InputManagerBase<Prescription>
    {
        private List<VisitContent> _visits { get; set; }
        private OrderedDictionary _options { get; set; }

        public GivePrescriptionsInput(MediClient client, IStreamIO streamIO, List<VisitContent> visits)
            : base(client, streamIO)
        {
            _visits = visits;
            _options = GetOptions();
        }

        public override void PrintInstructions()
        {
            _streamIO.TextElement.Interact("Select a visit");
        }

        public override Prescription GetInput()
        {
            Visit visit = _streamIO.ListElement.Interact(_options) as Visit;

        }

        private OrderedDictionary GetOptions()
        {
            var options = new OrderedDictionary();
            foreach (VisitContent visit in _visits)
            {
                string optionName = $"A visit with {visit.PersonName} at {visit.Visit.StartTime}";
                options.Add(optionName, visit.Visit);
            }

            return options;
        }
    }
}
