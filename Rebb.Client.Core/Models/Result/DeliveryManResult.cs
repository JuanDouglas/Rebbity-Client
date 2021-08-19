using System;

namespace Rebb.Client.Core.Models.Result
{
    public class DeliveryManResult
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDay { get; set; }
        public bool FixLocation { get; set; }
        public bool IsAproved { get; set; }
        public bool CNHInserted { get; set; }
        public bool RGInserted { get; set; }
    }
}