namespace AcademiaChallenge.Exceptions
{
    public class TotalConIVAInvalidoException : ValidacionFacturaException
    {
        public TotalConIVAInvalidoException() : base("El total es incorrecto")
        {
        }
    }
}
