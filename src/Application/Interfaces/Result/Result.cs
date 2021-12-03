using System.Threading.Tasks;

namespace EvrenDev.Application.Interfaces.Result
{
    public class Result : IResult
    {
        public Result()
        {
        }

        public string Message { get; set; }

        public bool Error { get; set; }

        public static IResult Fail()
        {
            return new Result { Error = true };
        }

        public static IResult Fail(string message)
        {
            return new Result { Error = true, Message = message };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static IResult Success()
        {
            return new Result { Error = false };
        }

        public static IResult Success(string message)
        {
            return new Result { Error = false, Message = message };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public Result()
        {
        }

        public T Data { get; set; }

        public static new Result<T> Fail()
        {
            return new Result<T> { Error = true };
        }

        public static new Result<T> Fail(string message)
        {
            return new Result<T> { Error = true, Message = message };
        }

        public static new Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static new Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static new Result<T> Success()
        {
            return new Result<T> { Error = false };
        }

        public static new Result<T> Success(string message)
        {
            return new Result<T> { Error = false, Message = message };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Error = false, Data = data };
        }

        public static Result<T> Success(T data, string message)
        {
            return new Result<T> { Error = false, Data = data, Message = message };
        }

        public static new Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static new Task<Result<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }
}
