namespace Energy.Helpers.Result;

public class Result<T> : Result
{
    internal Result(T? value, string? error) : base(value, error) { }

    public T Value { get => GetValue(); }

    private T GetValue()
    {
        if (IsFailure)
        {
            throw new InvalidCastException("Result is not Success. Casting to generic type is only available for successful results. Please, check the IsSuccess or IsFailure before casting to ensure correct state.");
        }

        return (T)_value!;
    }
}

public class Result
{
    protected readonly object? _value;
    protected readonly string? _error;

    private static readonly Result SuccessResult = new(null, null);

    protected Result(object? value, string? error)
    {
        _value = value;
        _error = error;
    }

    public bool IsFailure
    {
        get => _error is not null;
    }

    public string? Error
    {
        get => GetError();
    }

    private string GetError()
    {
        if (!IsFailure)
        {
            throw new InvalidOperationException("Result is not Failure. Successful results does not have Error objects.");
        }

        return _error!;
    }

    public static Result Success() => SuccessResult;

    public static Result<T> Success<T>(T value) => new(value, null);

    public static Result Failure(string error) => new(default, error);

    public static Result<T> Failure<T>(string error) => new(default, error);
}