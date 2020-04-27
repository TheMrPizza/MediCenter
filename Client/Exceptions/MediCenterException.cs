using System;

namespace Client.Exceptions
{
    public class MediCenterException : Exception
    {
        public MediCenterException() : base()
        {

        }

        public MediCenterException(string msg) : base(msg)
        {

        }
    }
}
