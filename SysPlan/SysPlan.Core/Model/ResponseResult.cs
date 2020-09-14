namespace SysPlan.Core.Model
{
    public class ResponseResult
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public object data { get; set; }
    }
}
