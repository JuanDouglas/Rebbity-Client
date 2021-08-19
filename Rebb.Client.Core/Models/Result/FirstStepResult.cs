using System;

namespace Rebb.Client.Core.Models.Result
{
    public class FirstStepResult
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Key { get; set; }
        public bool Valid { get; set; }
    }
}