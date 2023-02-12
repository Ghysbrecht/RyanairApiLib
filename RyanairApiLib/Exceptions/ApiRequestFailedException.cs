using System;

namespace RyanairApiLib.Exceptions
{
    public class ApiRequestFailedException : Exception
    {
        public string FullJsonResponse { get; }

        internal ApiRequestFailedException(string message, string FullJsonResponse = null) 
            : base(message)
        {
            this.FullJsonResponse = FullJsonResponse;
        }
    }
}
