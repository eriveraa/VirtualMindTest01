namespace EMT.Common.ResponseWrappers
{
    public class BaseResult<T>
    {
        public T Data { get; set; }

        public BaseResult()
        {
        }
    }
}
