namespace Client.Exceptions
{
    public class ExistedUsernameException : MediCenterException
    {
        public ExistedUsernameException() : base()
        {

        }

        public ExistedUsernameException(string msg) : base(msg)
        {

        }
    }
}
