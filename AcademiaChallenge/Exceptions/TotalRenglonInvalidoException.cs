namespace AcademiaChallenge.Exceptions
{
    public class TotalRenglonInvalidoException : ValidacionFacturaException
    {
        public TotalRenglonInvalidoException() : base("El total es incorrecto")
        {
        }
    }
}
