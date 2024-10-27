namespace AcademiaChallenge.Model
{
    public class Factura
    {
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public required string CodigoCliente { get; set; }
        public required string RazonSocial { get; set; }
        public required string Cuil { get; set; }
        public double ImporteTotalSinIva { get; set; }
        public double PorcentajeIva { get; set; }
        public double ImporteIva { get; set; }
        public double TotalConIva { get; set; }
        public required List<RenglonFactura> Renglones { get; set; }
    }
}
