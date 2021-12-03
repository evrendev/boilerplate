namespace EvrenDev.Application.Interfaces.Result
{
    public interface IResult
    {
        string Message { get; set; }

        bool Error { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}
