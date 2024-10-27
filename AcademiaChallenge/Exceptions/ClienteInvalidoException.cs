namespace AcademiaChallenge.Exceptions
{
    public class ClienteInvalidoException : ValidacionFacturaException
    {
        public ClienteInvalidoException() : base("El cliente ya existe con diferentes CUIL o porcentaje de IVA.")
        {
        }
    }
}