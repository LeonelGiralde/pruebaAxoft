namespace AcademiaChallenge.Exceptions
{
    public class NumeracionInvalidaException : ValidacionFacturaException
    {
        public NumeracionInvalidaException() : base("Numeración inválida")
        {
        }
    }
    public class FacturaFechaInvalidaException : ValidacionFacturaException
    {
        public FacturaFechaInvalidaException() : base("La factura no puede tener una fecha anterior a la ultima")
        {
        }
    }
    public class DatosIncorrectosInvalidaExceptions : ValidacionFacturaException
    {
        public DatosIncorrectosInvalidaExceptions() : base("Los datos de la factura no son correctos")
        {
        }
    }
    public class ArticuloIncorrectoInvalidaExceptions : ValidacionFacturaException
    {
        public ArticuloIncorrectoInvalidaExceptions() : base("Los datos del articulo no son correctos")
        {
        }
    }

    public class RenglonIncorrentoInvalidaExceptions : ValidacionFacturaException
    {
        public RenglonIncorrentoInvalidaExceptions() : base("Renglon invalido")
        {
        }
    }
    public class TotalDelRenglonInvalido : ValidacionFacturaException
    {
        public TotalDelRenglonInvalido() : base("El total del renglon no es el que se esperaba")
        {
        }
    }
    public class PorcentajeInvalido : ValidacionFacturaException
    {
        public PorcentajeInvalido() : base("El porcentaje de IVA es incorrecto")
        {
        }
    }
    public class ImporteIvaErroneo : ValidacionFacturaException
    {
        public ImporteIvaErroneo() : base("El importe de IVA es incorrecto")
        {
        }
    }
    public class ImporteTotalconIvaErroneo : ValidacionFacturaException
    {
        public ImporteTotalconIvaErroneo() : base("El importe total con IVA es incorrecto")
        {
        }
    }
    public class ArtriculoNoDisponible : ValidacionFacturaException
    {
        public ArtriculoNoDisponible() : base("No hay facturas")
        {
        }
    }
    public class ClienteNotFound : ValidacionFacturaException
    {
        public ClienteNotFound() : base("Cliente no encontrado")
        {
        }
    }
     public class NoSeEncontraronCompras : ValidacionFacturaException
    {
        public NoSeEncontraronCompras() : base("Compra no encontrada")
        {
        }
    }
     public class DescriptionNotFound : ValidacionFacturaException
    {
        public DescriptionNotFound() : base("Descripcion no encontrada")
        {
        }
    }

    public class NoHayFacturasEnEsaFecha : ValidacionFacturaException
    {
        public NoHayFacturasEnEsaFecha() : base("No hay facturas en la fecha selccionada")
        {
        }
    }
    
}

