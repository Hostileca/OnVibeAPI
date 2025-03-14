namespace Domain.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
    public ConflictException(Type type, string identifier) : base($"{type} with identifier {identifier} already exists") { }
}