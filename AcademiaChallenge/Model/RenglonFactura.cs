namespace AcademiaChallenge.Model
{
    public class RenglonFactura
    {
        public int NumeroRenglon { get; set; }
        public required string CodigoArticulo { get; set; }
        public required string DescripcionArtigulo { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }
    }
}
