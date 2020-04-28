using System.Collections.Generic;

namespace Common
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
