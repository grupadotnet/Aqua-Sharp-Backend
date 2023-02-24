namespace Aqua_Sharp_Backend.Exceptions;

public class BadRequest400Exception : Exception
{
    public BadRequest400Exception(string message) : base(message) { }
}