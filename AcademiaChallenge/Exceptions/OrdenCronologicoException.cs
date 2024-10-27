namespace AcademiaChallenge.Exceptions
{
    public class OrdenCronologicoException : ValidacionFacturaException
    {
        public OrdenCronologicoException() : base("Las facturas no están en orden cronológico.")
        {
        }
    }
}