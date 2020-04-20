namespace Common
{
    public class Medicine
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Medicine(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
