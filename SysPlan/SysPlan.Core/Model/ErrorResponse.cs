using System.Collections.Generic;

namespace SysPlan.Core.Model
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            details = new List<ErrorDetail>();
        }
        public string code { get; set; }
        public string message { get; set; }
        public string target { get; set; }
        public string stack { get; set; }
        public List<ErrorDetail> details { get; set; }
    }

    public class ErrorDetail
    {
        public string message { get; set; }
    }
}
