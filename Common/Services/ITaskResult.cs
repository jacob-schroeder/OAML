namespace Common.Services;

public interface ITaskResult
{
    public bool Success { get; }
    public ICollection<(string key, string error)> Errors { get; }
    public void CopyErrorsTo(ITaskResult target);
}