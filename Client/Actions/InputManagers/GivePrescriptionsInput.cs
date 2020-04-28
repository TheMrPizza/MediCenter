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
        private List<Medicine> _allMedicines { get; set; }
        private OrderedDictionary _options { get; set; }

        public GivePrescriptionsInput(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = GetOptions();
        }

        public override void Init(InputParams inputParams)
        {
            PrescriptionParams prescriptionParams = inputParams as PrescriptionParams;
            _visits = prescriptionParams.Visits;
            _allMedicines = prescriptionParams.Medicines;
        }

        public override void PrintInstructions()
        {
            _streamIO.TextElement.Interact("Select a visit");
        }

        public override Prescription GetInput()
        {
            Visit visit = _streamIO.ListElement.Interact(_options) as Visit;
            List<Medicine> medicines = GetMedicines();

            return new Prescription(visit.Id, medicines);
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

        private List<Medicine> GetMedicines()
        {
            var medicines = new List<Medicine>();
            _streamIO.TextElement.Interact("Enter the medicines:");
            foreach (Medicine m in _allMedicines)
            {
                if (_streamIO.FieldBooleanElement.Interact(m.Name))
                {
                    medicines.Add(m);
                }
            }

            return medicines;
        }
    }
}
