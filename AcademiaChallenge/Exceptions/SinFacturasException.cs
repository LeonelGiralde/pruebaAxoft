namespace AcademiaChallenge.Exceptions
{
    public class SinFacturaException : ValidacionFacturaException
    {
        public SinFacturaException() : base("No se puede determinar el artículo más vendido porque no hay facturas.")
        {
        }
    }
}
