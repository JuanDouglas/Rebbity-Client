using System;

namespace Rebb.Client.Core.Exceptions
{
    public class LoginException : Exception
    {
        public string FieldName { get; set; }
        public string Error { get; set; }

        public LoginException(string fieldName, string error)
        {
            FieldName = fieldName;
            Error = error;
        }
    }
}