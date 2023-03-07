namespace Aqua_Sharp_Backend.Exceptions;

public class RequestTimeout408Exception : Exception
{
    public RequestTimeout408Exception(string message) : base(message) { }
}