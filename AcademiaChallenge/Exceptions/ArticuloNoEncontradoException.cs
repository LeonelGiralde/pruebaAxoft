namespace AcademiaChallenge.Exceptions
{
    public class ArticuloNoEncontradoException : ValidacionFacturaException
    {
        public ArticuloNoEncontradoException() : base("El artículo no existe")
        {
        }
    }
}
