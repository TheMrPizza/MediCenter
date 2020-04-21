namespace Client.IO.Abstract
{
    public abstract class FieldElementBase
    {
        protected string _fieldName { get; set; }
        protected string _input { get; set; }

        public string Interact(string fieldName)
        {
            _fieldName = fieldName;
            PrintFieldName();
            return ReadInput();
        }

        protected abstract void PrintFieldName();
        protected abstract string ReadInput();
    }
}
