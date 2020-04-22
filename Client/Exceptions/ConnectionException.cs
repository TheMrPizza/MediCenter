namespace Client.Exceptions
{
    public class ConnectionException : MediCenterException
    {
        public ConnectionException() : base()
        {

        }

        public ConnectionException(string msg) : base(msg)
        {

        }
    }
}
