namespace Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Type type, string identifier) : base($"{type} with identifier {identifier} not found"){}
    public NotFoundException(string message) : base(message){}
}