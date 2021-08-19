using System.Collections.Generic;

namespace Rebb.Client.Core.Models.Result
{
    public class ValidationErrorsResult
    {
        public IDictionary<string, string[]> Errors { get; set; }
    }
}