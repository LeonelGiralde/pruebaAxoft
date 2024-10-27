namespace AcademiaChallenge.Exceptions
{
    public class ClienteNoEncontradoException : ValidacionFacturaException
    {
        public ClienteNoEncontradoException() : base("No se encontró el cliente")
        {
        }
    }
}