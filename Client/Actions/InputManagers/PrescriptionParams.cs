using System.Collections.Generic;
using Common;

namespace Client.Actions.InputManagers
{
    public class PrescriptionParams : InputParams
    {
        public List<VisitContent> Visits { get; set; }
        public List<Medicine> Medicines { get; set; }

        public PrescriptionParams(List<VisitContent> visits, List<Medicine> medicines)
        {
            Visits = visits;
            Medicines = medicines;
        }
    }
}
