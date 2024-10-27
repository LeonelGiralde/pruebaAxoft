namespace AcademiaChallenge.Exceptions
{
    public class SinArticulosException : ValidacionFacturaException
    {
        public SinArticulosException() : base("No se encontraron artículos válidos en las facturas para determinar el artículo más vendido.")
        {
        }
    }
}
