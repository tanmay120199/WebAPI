namespace RestAPIAsg.Middleware
{
    internal class GlobalErrorDetails
    {
        public GlobalErrorDetails()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}