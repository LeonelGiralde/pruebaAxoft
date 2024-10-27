namespace AcademiaChallenge.Exceptions
{
    public class ArticuloInvalidoException : ValidacionFacturaException
    {
        public ArticuloInvalidoException() : base("El artículo no es consistente en todas las facturas.")
        {
        }
    }
}
