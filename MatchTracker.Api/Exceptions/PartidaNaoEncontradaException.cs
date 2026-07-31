namespace MatchTracker.Api.Exceptions
{
    public class PartidaNaoEncontradaException : Exception
    {
        public PartidaNaoEncontradaException(string message) : base(message) { }
    }
}
