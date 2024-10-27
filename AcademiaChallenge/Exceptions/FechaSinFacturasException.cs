namespace AcademiaChallenge.Exceptions
{
    public class FechaSinFacturasException : ValidacionFacturaException
    {
        public FechaSinFacturasException() : base("No existen facturas en la fecha solicitada")
        {
        }
    }
}
