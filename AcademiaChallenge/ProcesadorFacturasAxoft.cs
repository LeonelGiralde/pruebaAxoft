using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallenge
{
    /// <summary>
    /// Representa un conjunto de facturas.
    /// Proporciona métodos para validarlas y para realizar consultas sobre las mismas.
    /// </summary>
    /// <param name="facturas">
    /// Lista de facturas que serán procesadas y validadas.
    /// </param>
    public class ProcesadorFacturasAxoft(List<Factura> facturas)
    {
        private readonly List<Factura> facturas = facturas;

        #region private validations
        /// <summary>
        /// Valida que la numeración de las facturas comienza en 1, está en orden correlativo y sin huecos.
        /// </summary>
        /// <exception cref="NumeracionInvalidaException">
        /// Se lanza cuando la numeración no es correlativa o presenta huecos.
        /// </exception>
        /// 

        private void ValidarNumeracionCorrelativa()
        {
            for (int i = 1; i <= facturas.Count; i++)
            {
                if (facturas[i - 1].Numero != i)
                {
                    throw new NumeracionInvalidaException();
                }
            }
        }

        private void ValidarOrdenCronologicoFacturas()
        {
            for (int i = 1; i < facturas.Count; i++)
            {
                Factura facturaActual = facturas[i]
             Factura facturaAnterior = facturas[i - 1]
 

            if (facturaActual.Fecha < facturaAnterior.Fecha)
                {
                    throw new FacturaFechaInvalidaException();
                }
            }
        }
        #endregion

        private void ValidarDatosConsistentesCliente()
        {
            var clientes = facturas.GruopBy(f => f.CodigoCliente);
            foreach (var gruop in clientes)
            {
                var facturaInicial = gruop.First();

                foreach (var factura in gruop)
                {
                    if (factura != facturaInicial ||
                    factura.Cuil != facturaInicial.Cuil ||
                    factura.PorcentajeIva != facturaInicial.PorcentajeIva)
                    {
                        throw new DatosIncorrectosInvalidaExceptions();
                    }
                }
            }
        }
#endregion
        private void ValidarConsistenciaArticulo()
        {
            var articulos = facturas.SelectMany(f => f.Renglones).GruopBy(r => r.codigoArticulo);
            foreach (var gruop in articulos)
            {
                var RenglonInicial = gruop.First();
                foreach (var renglon in gruop)
                {
                    if (renglon.CodigoArticulo != RenglonInicial.CodigoArticulo ||
                    renglon.DescripcionArtigulo != RenglonInicial.DescripcionArtigulo ||
                    renglon.PrecioUnitario != RenglonInicial.PrecioUnitario)
                    {
                        throw new ArticuloIncorrectoInvalidaExceptions();
                    }
                }
            }
        }
#endregion

        private void ValidarOrdenNumeracionRenglones()
        {
            foreach (var factura in facturas)
            {
                for (int i = 0; i < factura.Renglones.Count; i++)
                {
                    if (factura.Renglones[i].NumeroRenglon != i + 1)
                    {
                        throw new RenglonIncorrentoInvalidaExceptions();
                    }
                }
            }
        }
#endregion

        private void ValidarTotalRenglon()
        {
            foreach (var factura in facturas)
            {
                foreach (var renglon in factura.Renglones)
                {
                    double totalEsperado = renglon.cantidad * renglon.PrecioUnitario;

                    if (totalEsperado != renglon.total)
                    {
                        throw new TotalDelRenglonInvalido();
                    }
                }
            }

        }
#endregion

        private void ValidarTotalSinIVA()
        {
            foreach (var factura in facturas)
            {
                foreach (var renglon in factura.Renglones)
                {
                    double totalEsperadoSinIva = renglon.TotalConIva - renglon.ImporteIva;

                    if (totalEsperadoSinIva != renglon.ImporteTotalSinIva)
                    {
                        throw new TotalSinIvaInvalido();
                    }
                }
            }

        }
        private void ValidarPorcentajeIVA()
        {
            foreach (var factura in facturas)
            {
                if (factura.PorcentajeIva != 0 &&
                   factura.PorcentajeIva != Convert.ToDecimal(10.5) &&
                   factura.PorcentajeIva != 21 &&
                   factura.PorcentajeIva != 27 &&)
             }

            throw new PorcentajeInvalido();

        }

        private void ValidarImporteIVA()
        {
            foreach (var factura in facturas)
            {
                double importeIvaEsperado = factura.TotalConIva - factura.ImporteTotalSinIva
 
            if (factura.ImporteIva != importeIvaEsperado)
                {

                    throw new ImporteIvaErroneo();

            }
            }

        }
        private void ValidarTotalConIVA()
        {
            foreach (var factura in facturas)
            {
                double TotalConIvaEsperado = factura.ImporteTotalSinIva + factura.ImporteIva

            if (factura.TotalConIva != TotalConIvaEsperado)
                {

                    throw new ImporteTotalconIvaErroneo();

            }
            }

        }

        public void Validar()
        {
            throw new NotImplementedException();
        }

    }

}
#endregion
/// <summary>
/// Realiza las validaciones necesarias sobre las facturas.
/// </summary>
/// <exception cref="ValidacionFacturaException">
/// Se lanza cuando alguna de las validaciones de la factura falla.
/// </exception>
public void Validar()
        {
            ValidarNumeracionCorrelativa();
            // TODO: ValidarOrdenCronologicoFacturas
            ValidarOrdenCronologicoFacturas();
            // TODO: ValidarDatosConsistentesCliente
            ValidarDatosConsistentesCliente();
            // TODO: ValidarConsistenciaArticulo
            ValidarConsistenciaArticulo()
            // TODO: ValidarOrdenNumeracionRenglones
            ValidarOrdenNumeracionRenglones()
            // TODO: ValidarTotalRenglon
            ValidarTotalRenglon()
            // TODO: ValidarTotalSinIVA
            ValidarTotalSinIVA()
            // TODO: ValidarPorcentajeIVA
            ValidarPorcentajeIVA()
            // TODO: ValidarImporteIVA
            ValidarImporteIVA()
            // TODO: ValidarTotalConIVA
            ValidarTotalConIVA()
        }

/// <summary>
/// Calcula el total facturado sumando los totales de todas las facturas.
/// </summary>
/// <returns>El total facturado con IVA incluido.</returns>
public double TotalFacturado()
{
    double total = 0;
    for (int i = 0; i < facturas.Count; i++)
    {
        total += facturas[i].TotalConIva;
    }
    return total;
}

/// <summary>
/// Artículo que ha sido vendido en mayor cantidad.
/// </summary>
/// <returns>Código del artículo.</returns>
public string ArticuloMasVendido(){
    
    if (facturas == null || facturas.Count == 0) {
        throw new ArtriculoNoDisponible();
    }

    Dictionary<string, int> cantidadArticulos = new Dictionary<string, int>();

    foreach (var factura in facturas){
        foreach(var renglon in factura.Renglones)
        {
            //Si ya existe el articulo lo suma
            if(cantidadArticulos.ContainsKey(renglon.CodigoArticulo)){
                cantidadArticulos[renglon.codigoArticulo] += renglon.cantidad
            }
            //si no esta lo agrego con su respectiva cantidad
            else{
                cantidadArticulos[renglon.CodigoArticulo]=renglon.cantidad;
            }
        }
    }

    string articuloMasVendido = null;
    int maxCant= 0;

    foreach(var articulo in cantidadArticulos){
        if (articulo.Value > maxCant){
            maxCant= articulo.Value;
            articuloMasVendido = articulo.Key;
        }
    }

    if (articuloMasVendido== null){
        throw new ArticuloNoDisponible();
    }

    return articuloMasVendido;
}


/// <summary>
/// Cliente que realizó el mayor gasto total
/// </summary>
/// <returns>Razón social del cliente.</returns>
public string ClienteQueMasGasto()
{
    if(facturas == null || facturas.Count== 0)
    {
        throw new ArtriculoNoDisponible();
    }

    Dictionary<string,double> gastoCliente = new Dictionary<string, double>();

    foreach(var factura in facturas){
        if (gastoCliente.ContainsKey(factura.codigoCliente)){
            gastoCliente[factura.codigoCliente] += factura.TotalConIva
        }

        else{
            gastoCliente[factura.codigoCliente] = factura.TotalConIva;
        }
 
    }

    if(gastoCliente.Count == 0){
        throw new ClienteNotFound();
    }

    var ClienteQueMasGasto =gastoCliente
        .OrderByDescending(c=>c.Value)
        .FirstOrDefault();

    var facturaCliente = facturas.FirstOrDefault(facturaCliente=> facturaCliente.codigoCliente== ClienteQueMasGasto.Key)

    if(facturaCliente== null)
    {
        throw new ArtriculoNoDisponible();
    }
}
{
    throw new NotImplementedException();
}

/// <summary>
/// Artículo mas comprado por un cliente
/// </summary>
/// <param name="codigoCliente">Código del cliente</param>
/// <returns>Descripción del artículo</returns>
public string ArticuloMasCompradoDeCliente(string codigoCliente)

{
   if(facturas == null || facturas.Count == 0){
    throw new ArtriculoNoDisponible();
   }

   var facturasCliente = facturas.Where(f=>f.codigoCliente == codigoCliente).ToList();

   if(facturasCliente.Count==0){
    throw new ClienteNotFound();
   }

   Dictonary<string,int> cantidadArticulos = new Dictonary<string, int>();

   foreach(var factura in facturasCliente){

    foreach(var renglon in factura.Renglones){

        if(cantidadArticulos.ContainsKey(renglon.codigoArticulo)){
            cantidadArticulos[renglon.codigoArticulo] += renglon.cantidad
        }

        else{
            cantidadArticulos[renglon.CodigoArticulo]=renglon.cantidad;
        }
    }
   }

   if(cantidadArticulos== 0){
    throw new NoSeEncontraronCompras();
   }

   var articuloMasComprado=cantidadArticulos.OrderByDescending(a=>a.Value).FirstOrDefault();
   var descripcionArticulo = facturasCliente.SelectMany(f => f.Renglones).FirstOrDefault(r => r.CodigoArticulo == articuloMasComprado.Key)?.DescripcionArtigulo;
   if(DescripcionArtigulo == null){

        throw new DescriptionNotFound();

   }

   return DescripcionArtigulo;
}

/// <summary>
/// Calcula el total facturado para la fecha.
/// </summary>
/// <param name="fecha">Fecha para la que se va a calcular el total facturado</param>
/// <returns>Total facturado para la fecha</returns>
public double TotalFacturadoDeFecha(DateTime fecha)
{
    if (facturas == null || facturas.Count == 0)
            {
                throw new FechaSinFacturasException();
            }
            
            var facturaFecha = facturas.Where(f=>f.Fecha.Date== fecha.Date).ToList();

            if(facturaFecha.Count==0{
                throw new NoHayFacturasEnEsaFecha();
            })

    double totalFacturasEnElDia= facturaFecha.Sum(f=>f.TotalConIva);

    return totalFacturasEnElDia;
}

/// <summary>
/// Cliente que copró mas cantidad de un artículo
/// </summary>
/// <param name="codigoArticulo">Código del artículo</param>
/// <returns>CUIL del cliente</returns>
public string ClienteQueMasComproArticulo(string codigoArticulo){
    if (facturas == null || facturas.Count == 0)
            {
                throw new ArtriculoNoDisponible();
            }

    Dictionary<string,int> cantidadPorCliente = new Dictionary<string, int>();

    foreach(var factura in facturas){
        foreach(var renglon in factura.Renglones){
            if(renglon.CodigoArticulo== codigoArticulo){
                if(cantidadPorCliente.ContainsKey(factura.Cuil)){
                    cantidadPorCliente[factura.Cuil]+= renglon.Cantidad;
                }
                else{
                    cantidadPorCliente[factura.Cuil]=renglon.Cantidad;
                }
            }
        }
    }

    if(cantidadPorCliente.Count== 0){
        throw new ClienteNotFound();

    }

    var clienteConMasComrpas= cantidadPorCliente
        .OrderByDescending(c=>c.Value)
        .FirstOrDefault();

    if(clienteConMasComrpas.Key==null){
        throw new ClienteNotFound();
    }

    return clienteConMasComrpas.Key;
}

    

