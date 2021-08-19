using System;

namespace Rebb.Client.Core.Models.Upload
{
    public class DeliveryManUpload
    {
        public string CPF { get; set; }
        public DateTime BirthDay { get; set; }
        public bool FixLocation { get; set; }
    }
}