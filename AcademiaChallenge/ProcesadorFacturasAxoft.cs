using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallenge
{
    public class ProcesadorFacturasAxoft
    {
        private readonly List<Factura> facturas;

        public ProcesadorFacturasAxoft(List<Factura> facturas)
        {
            this.facturas = facturas ?? new List<Factura>();
        }

        #region private validationes
        /// <summary>
        /// Valida que la numeración de las facturas comienza en 1, está en orden correlativo y sin huecos.
        /// </summary>
        /// <exception cref="NumeracionInvalidaException">
        /// Se lanza cuando la numeración no es correlativa o presenta huecos.
        /// </exception>
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

        /// <summary>
        /// Valida que las facturas estén en orden cronológico.
        /// </summary>
        /// <exception cref="OrdenCronologicoException">
        /// Se lanza cuando las facturas no están en orden cronológico.
        /// </exception>
        private void ValidarOrdenCronologicoFacturas()
        {
            for (int i = 1; i < facturas.Count; i++)
            {
                if (facturas[i].Fecha < facturas[i - 1].Fecha)
                {
                    throw new OrdenCronologicoException();
                }
            }
        }
        /// <summary>
        /// Valida que el cliente debe tener siempre el mismo CUIL, CódigoCliente y porcentaje de IVA.
        /// </summary>
        /// <exception cref="ClienteInvalidoException">
        /// Se lanza cuando el CUIL o el porcentaje de IVA del cliente no son consistentes.
        /// </exception>
        private void ValidarDatosConsistentesCliente()
        {
            var clientesAgrupados = facturas.GroupBy(f => f.CodigoCliente);

            foreach (var grupo in clientesAgrupados)
            {
                var primeraFactura = grupo.First();

                foreach (var factura in grupo)
                {
                    if (factura != primeraFactura &&
                        (factura.Cuil != primeraFactura.Cuil ||
                        factura.PorcentajeIva != primeraFactura.PorcentajeIva))
                    {
                        throw new ClienteInvalidoException();
                    }
                }
            }
        }

        /// <summary>
        /// Valida que un mismo artículo tenga el mismo código, descripción y precio unitario en todas las facturas.
        /// </summary>
        /// <exception cref="ArticuloInvalidoException">
        /// Se lanza cuando el código, la descripción o el precio del artículo no son consistentes.
        /// </exception>
        private void ValidarConsistenciaArticulo()
        {
            // Agrupar todos los renglones de todas las facturas por código de artículo
            var articulosAgrupados = facturas.SelectMany(f => f.Renglones).GroupBy(r => r.CodigoArticulo);

            foreach (var grupo in articulosAgrupados)
            {
                var primerRenglon = grupo.First(); // Obtenemos el primer renglón para comparar

                foreach (var renglon in grupo)
                {
                    // Validamos si hay inconsistencia en el código, descripción o precio unitario
                    if (renglon.CodigoArticulo != primerRenglon.CodigoArticulo ||
                        renglon.DescripcionArticulo != primerRenglon.DescripcionArticulo ||
                        renglon.PrecioUnitario != primerRenglon.PrecioUnitario)
                    {
                        throw new ArticuloInvalidoException();
                    }
                }
            }
        }

        /// <summary>
        /// Valida que el orden de numeración de los renglones dentro de cada factura sea correcto.
        /// La numeración de los renglones debe comenzar en 1 y ser consecutiva.
        /// </summary>
        /// <exception cref="NumeracionRenglonesInvalidaException">
        /// Se lanza cuando la numeración de los renglones en una factura no es consecutiva.
        /// </exception>
        private void ValidarOrdenNumeracionRenglones()
        {
            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Recorre todos los renglones de la factura
                for (int i = 0; i < factura.Renglones.Count; i++)
                {
                    // Verifica si la numeración del renglón es correcta
                    // Como i empieza en 0, el número esperado es i + 1
                    if (factura.Renglones[i].NumeroRenglon != i + 1)
                    {
                        throw new NumeracionRenglonesInvalidaException();
                    }
                }
            }
        }

        /// <summary>
        /// Valida que el total de cada renglón en todas las facturas esté calculado correctamente.
        /// El total debe ser igual a PrecioUnitario * Cantidad.
        /// </summary>
        /// <exception cref="TotalRenglonInvalidoException">
        /// Se lanza cuando el total de un renglón no está calculado correctamente.
        /// </exception>
        private void ValidarTotalRenglon()
        {
            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Recorre todos los renglones de la factura
                foreach (var renglon in factura.Renglones)
                {
                    // Calcula el total esperado (PrecioUnitario * Cantidad)
                    double totalEsperado = renglon.PrecioUnitario * renglon.Cantidad;

                    // Verifica si el total calculado es igual al total del renglón
                    if (renglon.Total != totalEsperado)
                    {
                        throw new TotalRenglonInvalidoException();
                    }
                }
            }
        }

        /// <summary>
        /// Valida que el total sin IVA de cada factura sea correcto.
        /// El total sin IVA debe ser igual a la suma de los totales de todos los renglones.
        /// </summary>
        /// <exception cref="TotalSinIVAInvalidoException">
        /// Se lanza cuando el total sin IVA de una factura no es correcto.
        /// </exception>
        private void ValidarTotalSinIVA()
        {
            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Suma los totales de todos los renglones de la factura
                double totalSinIVAEsperado = factura.Renglones.Sum(r => r.Total);

                // Verifica si el total sin IVA es correcto
                if (factura.ImporteTotalSinIva != totalSinIVAEsperado)
                {
                    throw new TotalSinIVAInvalidoException();
                }
            }
        }

        /// <summary>
        /// Valida que el porcentaje de IVA de cada factura sea uno de los valores permitidos (0%, 10.5%, 21%, 27%).
        /// </summary>
        /// <exception cref="PorcentajeIVAInvalidoException">
        /// Se lanza cuando el porcentaje de IVA de una factura no es válido.
        /// </exception>
        private void ValidarPorcentajeIVA()
        {
            // Lista de valores de IVA permitidos
            double[] porcentajesIvaPermitidos = { 0, 10.5, 21, 27 };

            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Verifica si el porcentaje de IVA no está en la lista de valores permitidos
                if (!porcentajesIvaPermitidos.Contains(factura.PorcentajeIva))
                {
                    throw new PorcentajeIVAInvalidoException();
                }
            }
        }

        /// <summary>
        /// Valida que el importe del IVA de cada factura sea correcto.
        /// El importe del IVA debe ser igual al total sin IVA multiplicado por el porcentaje de IVA.
        /// </summary>
        /// <exception cref="ImporteIVAInvalidoException">
        /// Se lanza cuando el importe del IVA de una factura no es correcto.
        /// </exception>
        private void ValidarImporteIVA()
        {
            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Calcula el importe del IVA esperado
                double importeIvaEsperado = factura.ImporteTotalSinIva * (factura.PorcentajeIva / 100);

                // Verifica si el importe de IVA es correcto
                if (factura.ImporteIva != importeIvaEsperado)
                {
                    throw new TotalSinIVAInvalidoException();
                }
            }
        }

        /// <summary>
        /// Valida que el total con IVA de cada factura sea correcto.
        /// El total con IVA debe ser igual al total sin IVA más el importe del IVA.
        /// </summary>
        /// <exception cref="TotalConIVAInvalidoException">
        /// Se lanza cuando el total con IVA de una factura no es correcto.
        /// </exception>
        private void ValidarTotalConIVA()
        {
            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Calcula el total con IVA esperado
                double totalConIVAEsperado = factura.ImporteTotalSinIva + factura.ImporteIva;

                // Verifica si el total con IVA es correcto
                if (factura.TotalConIva != totalConIVAEsperado)
                {
                    throw new TotalConIVAInvalidoException();
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
            ValidarOrdenCronologicoFacturas();
            ValidarDatosConsistentesCliente();
            ValidarConsistenciaArticulo();
            ValidarOrdenNumeracionRenglones();
            ValidarTotalRenglon();
            ValidarTotalSinIVA();
            ValidarPorcentajeIVA();
            ValidarImporteIVA();
            ValidarTotalConIVA();
        }

        #region Consultas  
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
        /// Determina el artículo que ha sido vendido en mayor cantidad en todas las facturas.
        /// </summary>
        /// <returns>Código del artículo más vendido.</returns>
        /// <exception cref="ArticuloNoEncontradoException">
        /// Se lanza cuando no se encuentran artículos en las facturas.
        /// </exception>
        public string ArticuloMasVendido()
        {
            // Verificar si hay facturas
            if (facturas == null || facturas.Count == 0)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Diccionario para almacenar la cantidad vendida de cada artículo (clave: código del artículo)
            Dictionary<string, int> cantidadArticulos = new Dictionary<string, int>();

            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Recorre todos los renglones de la factura
                foreach (var renglon in factura.Renglones)
                {
                    // Si el artículo ya está en el diccionario, suma la cantidad
                    if (cantidadArticulos.ContainsKey(renglon.CodigoArticulo))
                    {
                        cantidadArticulos[renglon.CodigoArticulo] += renglon.Cantidad;
                    }
                    // Si el artículo no está en el diccionario, lo agrega con la cantidad inicial
                    else
                    {
                        cantidadArticulos[renglon.CodigoArticulo] = renglon.Cantidad;
                    }
                }
            }

            // Verificar si hay artículos en el diccionario
            if (cantidadArticulos.Count == 0)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Identificar el artículo con la mayor cantidad vendida
            var articuloMasVendido = cantidadArticulos
                .OrderByDescending(a => a.Value)
                .FirstOrDefault();

            // Verificar si se encontró un artículo más vendido
            if (articuloMasVendido.Key == null)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Retornar el código del artículo más vendido
            return articuloMasVendido.Key;
        }

        /// <summary>
        /// Devuelve la razón social del cliente que realizó el mayor gasto total.
        /// </summary>
        /// <returns>Razón social del cliente con el mayor gasto total.</returns>
        /// <exception cref="ClienteNoEncontradoException">
        /// Se lanza cuando no se encuentra un cliente con gasto.
        /// </exception>
        public string ClienteQueMasGasto()
        {
            // Verificar si hay facturas
            if (facturas == null || facturas.Count == 0)
            {
                throw new ClienteNoEncontradoException();
            }

            // Diccionario para almacenar el gasto total de cada cliente (clave: código del cliente)
            Dictionary<string, double> gastoClientes = new Dictionary<string, double>();

            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Si el cliente ya está en el diccionario, suma el total con IVA
                if (gastoClientes.ContainsKey(factura.CodigoCliente))
                {
                    gastoClientes[factura.CodigoCliente] += factura.TotalConIva;
                }
                // Si el cliente no está en el diccionario, lo agrega con el gasto inicial
                else
                {
                    gastoClientes[factura.CodigoCliente] = factura.TotalConIva;
                }
            }

            // Verificar si hay datos en el diccionario de clientes
            if (gastoClientes.Count == 0)
            {
                throw new ClienteNoEncontradoException();
            }

            // Identificar el cliente con el mayor gasto
            var clienteQueMasGasto = gastoClientes
                .OrderByDescending(c => c.Value)
                .FirstOrDefault();

            // Obtener la factura correspondiente al cliente que más gastó
            var facturaCliente = facturas.FirstOrDefault(f => f.CodigoCliente == clienteQueMasGasto.Key);

            // Verificar si se encontró la factura del cliente
            if (facturaCliente == null)
            {
                throw new ClienteNoEncontradoException();
            }

            // Retornar la razón social del cliente que más gastó
            return facturaCliente.RazonSocial;
        }

        /// <summary>
        /// Artículo más comprado por un cliente.
        /// </summary>
        /// <param name="codigoCliente">Código del cliente</param>
        /// <returns>Descripción del artículo más comprado por el cliente.</returns>
        /// <exception cref="ClienteNoEncontradoException">
        /// Se lanza cuando no se encuentra el cliente.
        /// </exception>
        /// <exception cref="ArticuloNoEncontradoException">
        /// Se lanza cuando el cliente no tiene artículos comprados.
        /// </exception>
        public string ArticuloMasCompradoDeCliente(string codigoCliente)
        {
            // Verificar si hay facturas
            if (facturas == null || facturas.Count == 0)
            {
                throw new ClienteNoEncontradoException();
            }

            // Filtrar las facturas del cliente específico
            var facturasCliente = facturas.Where(f => f.CodigoCliente == codigoCliente).ToList();

            // Verificar si el cliente tiene facturas
            if (facturasCliente.Count == 0)
            {
                throw new ClienteNoEncontradoException();
            }

            // Diccionario para almacenar la cantidad comprada de cada artículo (clave: código del artículo)
            Dictionary<string, int> cantidadArticulos = new Dictionary<string, int>();

            // Recorre todas las facturas del cliente
            foreach (var factura in facturasCliente)
            {
                // Recorre todos los renglones de la factura
                foreach (var renglon in factura.Renglones)
                {
                    // Si el artículo ya está en el diccionario, suma la cantidad
                    if (cantidadArticulos.ContainsKey(renglon.CodigoArticulo))
                    {
                        cantidadArticulos[renglon.CodigoArticulo] += renglon.Cantidad;
                    }
                    // Si el artículo no está en el diccionario, lo agrega con la cantidad inicial
                    else
                    {
                        cantidadArticulos[renglon.CodigoArticulo] = renglon.Cantidad;
                    }
                }
            }

            // Verificar si el cliente ha comprado algún artículo
            if (cantidadArticulos.Count == 0)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Identificar el artículo más comprado por el cliente
            var articuloMasComprado = cantidadArticulos
                .OrderByDescending(a => a.Value)
                .FirstOrDefault();

            // Obtener el primer renglón que coincida con el artículo más comprado
            var descripcionArticulo = facturasCliente
                .SelectMany(f => f.Renglones)
                .FirstOrDefault(r => r.CodigoArticulo == articuloMasComprado.Key)?.DescripcionArticulo;

            // Verificar si se encontró la descripción del artículo
            if (descripcionArticulo == null)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Retornar la descripción del artículo más comprado
            return descripcionArticulo;
        }

        /// <summary>
        /// Calcula el total facturado en una fecha específica.
        /// </summary>
        /// <param name="fecha">Fecha para la cual se va a calcular el total facturado.</param>
        /// <returns>Total facturado en la fecha especificada.</returns>
        /// <exception cref="FechaSinFacturasException">
        /// Se lanza si no se encuentran facturas en la fecha dada.
        /// </exception>
        public double TotalFacturadoDeFecha(DateTime fecha)
        {
            // Verificar si hay facturas
            if (facturas == null || facturas.Count == 0)
            {
                throw new FechaSinFacturasException();
            }

            // Filtrar las facturas que coincidan con la fecha proporcionada
            var facturasEnFecha = facturas.Where(f => f.Fecha.Date == fecha.Date).ToList();

            // Verificar si hay facturas en la fecha proporcionada
            if (facturasEnFecha.Count == 0)
            {
                throw new FechaSinFacturasException();
            }

            // Sumar los totales con IVA de las facturas en la fecha
            double totalFacturado = facturasEnFecha.Sum(f => f.TotalConIva);

            return totalFacturado;
        }

        /// <summary>
        /// Cliente que compró más cantidad de un artículo en particular.
        /// </summary>
        /// <param name="codigoArticulo">Código del artículo.</param>
        /// <returns>CUIL del cliente que compró la mayor cantidad del artículo.</returns>
        /// <exception cref="ArticuloNoEncontradoException">
        /// Se lanza cuando no se encuentran facturas que contengan el artículo.
        /// </exception>
        /// <exception cref="ClienteNoEncontradoException">
        /// Se lanza cuando no se encuentra un cliente que haya comprado el artículo.
        /// </exception>
        public string ClienteQueMasComproArticulo(string codigoArticulo)
        {
            // Verificar si hay facturas
            if (facturas == null || facturas.Count == 0)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Diccionario para almacenar la cantidad comprada de un artículo por cliente (clave: CUIL del cliente)
            Dictionary<string, int> cantidadPorCliente = new Dictionary<string, int>();

            // Recorre todas las facturas
            foreach (var factura in facturas)
            {
                // Recorre todos los renglones de la factura
                foreach (var renglon in factura.Renglones)
                {
                    // Si el artículo coincide con el código proporcionado
                    if (renglon.CodigoArticulo == codigoArticulo)
                    {
                        // Si el cliente ya está en el diccionario, suma la cantidad comprada
                        if (cantidadPorCliente.ContainsKey(factura.Cuil))
                        {
                            cantidadPorCliente[factura.Cuil] += renglon.Cantidad;
                        }
                        // Si el cliente no está en el diccionario, lo agrega con la cantidad inicial
                        else
                        {
                            cantidadPorCliente[factura.Cuil] = renglon.Cantidad;
                        }
                    }
                }
            }

            // Verificar si se encontraron clientes que hayan comprado el artículo
            if (cantidadPorCliente.Count == 0)
            {
                throw new ArticuloNoEncontradoException();
            }

            // Identificar el cliente que más cantidad compró del artículo
            var clienteQueMasCompro = cantidadPorCliente
                .OrderByDescending(c => c.Value)
                .FirstOrDefault();

            // Verificar si se encontró un cliente
            if (clienteQueMasCompro.Key == null)
            {
                throw new ClienteNoEncontradoException();
            }
            // Retornar el CUIL del cliente que más compró el artículo
            return clienteQueMasCompro.Key;
        }
        #endregion
    }
}
