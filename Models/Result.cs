public class Result<T>
{
    public T Value { get; }
    public string Error { get; }
    public bool IsSuccess => Error == null;

    public Result(T value)
    {
        Value = value;
    }

    public Result(string error)
    {
        Error = error;
    }
}