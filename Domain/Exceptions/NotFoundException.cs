namespace Domain.Exceptions;

public class NotFoundException(Type type, string identifier) : Exception($"{type} with identifier {identifier} not found");