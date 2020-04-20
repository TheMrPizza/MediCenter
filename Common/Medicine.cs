namespace Common
{
    public class Medicine : IModel
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
