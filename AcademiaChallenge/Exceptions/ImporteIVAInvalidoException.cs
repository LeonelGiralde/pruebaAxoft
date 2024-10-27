using AcademiaChallenge.Model;
using AcademiaChallenge.Exceptions;


namespace AcademiaChallenge.Exceptions
{
    namespace AcademiaChallenge.Exceptions
    {
        public class ImporteIVAInvalidoException : ValidacionFacturaException
        {
            public ImporteIVAInvalidoException() : base("El importe es incorrecto")
            {
            }
        }
    }
}
