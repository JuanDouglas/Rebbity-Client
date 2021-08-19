using Rebb.Client.Core.Models.Enums;

namespace Rebb.Client.Core.Models.Result
{
    public class FileResult
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
    }
}