using Rebb.Client.Core.Models.Result;
using System;

namespace Rebb.Client.Core.Exceptions
{
    public class ValidationErrorsException : Exception
    {
        public ValidationErrorsResult Errors { get; set; }

        public ValidationErrorsException(ValidationErrorsResult errors)
        {
            Errors = errors;
        }
    }
}