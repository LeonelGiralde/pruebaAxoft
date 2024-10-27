namespace AcademiaChallenge.Model
{
    public class RenglonFactura
    {
        public int NumeroRenglon { get; set; }
        public required string CodigoArticulo { get; set; }
        public required string DescripcionArticulo { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }
    }
}
