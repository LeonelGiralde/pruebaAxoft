using AcademiaChallenge.Model;

namespace AcademiaChallenge.Exceptions
{
    public class PorcentajeIVAInvalidoException : ValidacionFacturaException
    {
        public PorcentajeIVAInvalidoException() : base($"El porcentaje de IVAes inválido. Solo se permiten los valores 0%, 10.5%, 21%, y 27%.")
        {
        }
    }
}
