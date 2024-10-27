namespace AcademiaChallenge.Exceptions
{
    public class NumeracionRenglonesInvalidaException : ValidacionFacturaException
    {
        public NumeracionRenglonesInvalidaException() : base("La numeracion es inválida")
        {
        }
    }
}
