namespace Bakery.Application.Common.Models;

public class ServiceResult<TValue>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public TValue Value { get; set; }
    
    public static ServiceResult<TValue> Success(TValue value)
    {
        return new ServiceResult<TValue>
        {
            IsSuccess = true,
            Value = value
        };
    }
    
    public static ServiceResult<TValue> Failure(string message)
    {
        return new ServiceResult<TValue>
        {
            IsSuccess = false,
            Message = message
        };
    }
}