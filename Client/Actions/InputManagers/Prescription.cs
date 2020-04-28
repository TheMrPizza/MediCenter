using System.Collections.Generic;
using Common;

namespace Client.Actions.InputManagers
{
    public class Prescription
    {
        public string VisitId { get; set; }
        public List<Medicine> Medicines { get; set; }

        public Prescription(string visitId, List<Medicine> medicines)
        {
            VisitId = visitId;
            Medicines = medicines;
        }
    }
}
