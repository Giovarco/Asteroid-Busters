public enum Status
{
    Ok,
    Teleporting
};

public interface IHaveStatus
{
    Status status { get; }
}