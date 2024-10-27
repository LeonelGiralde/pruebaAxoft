namespace AcademiaChallenge.Exceptions
{
    public class TotalSinIVAInvalidoException : ValidacionFacturaException
    {
        public TotalSinIVAInvalidoException() : base("El total es incorrecto")
        {
        }
    }
}
