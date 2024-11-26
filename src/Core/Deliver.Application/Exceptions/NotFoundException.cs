namespace Deliver.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() : base("not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key) : base(
        $"{name} ({key}) is not found"
    )
    {
    }
}