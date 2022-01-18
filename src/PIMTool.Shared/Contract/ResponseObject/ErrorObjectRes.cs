using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.ResponseObject
{
    public class ErrorObjectRes
    {
        public ErrorObjectRes()
        {
            Time = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }

        public short Status { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public Dictionary<string, string> Errors { get; set; }

        public string Time { get; set; }

        public string RequestId { get; set; }

    }
}
