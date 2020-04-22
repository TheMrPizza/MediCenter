namespace Client.Exceptions
{
    public class NotFoundException : ConnectionException
    {
        public NotFoundException() : base()
        {

        }

        public NotFoundException(string msg) : base(msg)
        {

        }
    }
}
