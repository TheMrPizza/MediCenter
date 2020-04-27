using Common;

namespace Client.Actions.OutputManagers
{
    public class VisitContent
    {
        public Visit Visit { get; set; }
        public string PersonName { get; set; }
        public int Num { get; set; }

        public VisitContent(Visit visit, string personName, int num)
        {
            Visit = visit;
            PersonName = personName;
            Num = num;
        }
    }
}
