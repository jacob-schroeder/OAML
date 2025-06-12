namespace Common.Services;

public class TaskResult : ITaskResult
{
    public bool Success => Errors.Any();
    public ICollection<(string key, string error)> Errors { get; private set; } = [];

    public void AddError(string key, string error)
    {
        Errors.Add((key, error));
    }

    public void AddError(string error)
    {
        Errors.Add(("", error));
    }

    public void CopyErrorsTo(ITaskResult target)
    {
        foreach(var error in Errors)
            target.Errors.Add(error);
    }
}