namespace Client.IO.Abstract
{
    public abstract class TextElementBase
    {
        protected string _text { get; set; }

        public void Interact(string text)
        {
            _text = text;
            PrintText();
        }

        protected abstract void PrintText();
    }
}
