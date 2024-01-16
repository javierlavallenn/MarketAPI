namespace Market.Shared
{
    public class Result<T> where T : class
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
