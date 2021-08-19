using System;

namespace Rebb.Client.Core.Models.Upload
{
    [Serializable]
    public class AccountUpload
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool AcceptTerms { get; set; }
    }
}