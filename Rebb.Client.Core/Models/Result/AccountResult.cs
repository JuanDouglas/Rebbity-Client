using System;

namespace Rebb.Client.Core.Models.Result
{
    public class AccountResult
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsCompany { get; set; }
        public DateTime AcceptTermsDate { get; set; }
        public bool Valid { get; set; }
        public DateTime CreateDate { get; set; }
        public FileResult ProfileImage { get; set; }
    }
}