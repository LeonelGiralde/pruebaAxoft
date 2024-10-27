
using AcademiaChallenge.Model;
using AcademiaChallenge.Exceptions;

public class NumeracionInvalidaException : ValidacionFacturaException
{
    public NumeracionInvalidaException() : base("La numeracion es incorrecta")
    {
    }
}
