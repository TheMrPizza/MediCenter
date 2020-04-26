namespace Client.Exceptions
{
    public class RequestException : MediCenterException
    {
        public RequestException() : base()
        {

        }

        public RequestException(string msg) : base(msg)
        {

        }
    }
}
