namespace Commons.ResultHelper;
public class ResultHelper
{
    public bool Sucess { get; set; }

    public string? ErrorMessage { get; set; }

    public string? Message { get; set; }

    public ResultHelper Correct()
    {
        Sucess = true;

        return this;
    }

    public ResultHelper Failed()
    {
        Sucess = false;

        return this;
    }

    public ResultHelper Correct(string? message = null)
    {
        Sucess = true;
        Message = message;

        return this;
    }

    public ResultHelper Failed(string? errorMessage = null)
    {
        Sucess = false;
        ErrorMessage = errorMessage;

        return this;
    }
}

public class ResultHelper<T> : ResultHelper
{
    public T Value { get; set; }

    public ResultHelper<T> Correct(T value, string? message = null)
    {
        Sucess = true;
        Message = message;
        Value = value;

        return this;
    }

    public ResultHelper<T> Failed(T value, string? errorMessage = null)
    {
        Sucess = false;
        ErrorMessage = errorMessage;
        Value = value;

        return this;
    }
}
