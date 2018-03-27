namespace LiteRest.Testing
{

    public class ApiResult<T>
    {

        public T Value { get; }

        public ApiResult(T value)
        {
            Value = value;
        }

    }
}
