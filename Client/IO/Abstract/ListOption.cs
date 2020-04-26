namespace Client.IO.Abstract
{
    public class ListOption
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ListOption(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public ListOption(string name)
        {
            Name = name;
            Value = name;
        }
    }
}
