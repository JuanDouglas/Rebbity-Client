using System;

namespace Rebb.Client.Core.Models.Result
{
    public class ValidLoginResult
    {
        public DateTime ValidationDate { get; set; }
        public bool ValidedAccount { get; set; }
        public bool ValidLogin { get; set; }
    }
}