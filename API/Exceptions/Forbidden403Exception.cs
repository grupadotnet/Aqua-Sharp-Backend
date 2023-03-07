namespace Aqua_Sharp_Backend.Exceptions;

public class Forbidden403Exception : Exception
{
    public Forbidden403Exception(string message) : base(message) { }    
}