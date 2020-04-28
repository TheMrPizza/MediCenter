using System.Collections.Generic;
using Common;

namespace Client.Actions
{
    public class VisitContent
    {
        public Visit Visit { get; set; }
        public string PersonName { get; set; }
        public List<Medicine> Medicines { get; set; }
        public int Num { get; set; }

        public VisitContent(Visit visit, string personName, List<Medicine> medicines, int num)
        {
            Visit = visit;
            PersonName = personName;
            Medicines = medicines;
            Num = num;
        }

        public VisitContent(Visit visit, string personName)
        {
            Visit = visit;
            PersonName = personName;
        }
    }
}
