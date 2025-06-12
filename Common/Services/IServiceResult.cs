namespace Common.Services;

public interface IServiceResult<out TResult>: ITaskResult
{
    TResult Result { get; }
}