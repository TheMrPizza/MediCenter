namespace Common
{
    public class Medicine
    {
        public string ObjectId { get; set; }
        public string Name { get; set; }

        public Medicine(string objectId, string name)
        {
            ObjectId = objectId;
            Name = name;
        }
    }
}
