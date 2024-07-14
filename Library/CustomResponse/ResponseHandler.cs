namespace Library.CustomResponse
{
    public class ResponseHandler
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Object? Data { get; set; }
    }
}
