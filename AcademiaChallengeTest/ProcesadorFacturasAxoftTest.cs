using AcademiaChallenge;
using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallengeTest
{
    [TestClass]
    public class ProcesadorFacturasAxoftTest
    {
        #region Validar
        #region Validar casos correctos
        [TestMethod]
        public void Validar_CasoCorrecto00_SinFacturas_NoTiraExcepcion()
        {
            List<Factura> facturas = [];

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }

        [TestMethod]
        public void Validar_CasoCorrecto01_UnaFacturaUnRenglon_NoTiraExcepcion()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }

        [TestMethod]
        public void Validar_CasoCorrecto02_UnaFacturaDosRenglones_NoTiraExcepcion()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 5,
                        Total = 5
                    },
                    new RenglonFactura()
                    {
                        NumeroRenglon = 2,
                        CodigoArticulo = "Art02",
                        DescripcionArticulo = "SoyElArticulo2",
                        PrecioUnitario = 5,
                        Cantidad = 1,
                        Total = 5
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }

        [TestMethod]
        public void Validar_CasoCorrecto03_DosFacturas_NoTiraExcepcion()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 02),
                CodigoCliente = "Cli02",
                RazonSocial = "Cliente 02 SA",
                Cuil = "987654321",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }
        #endregion

        #region Validar numeración correlativa
        [TestMethod]
        public void Validar_NumeracionCorrelativa01_PimerFactura0TiraError()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 0,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<NumeracionInvalidaException>(procesadorFacturas.Validar, "Numeración inválida");
        }

        [TestMethod]
        public void Validar_NumeracionCorrelativa03_SegundaFactura3TiraError()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 3,
                Fecha = new DateTime(2024, 01, 02),
                CodigoCliente = "Cli02",
                RazonSocial = "Cliente 02 SA",
                Cuil = "987654321",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                            [
                                new RenglonFactura()
                                {
                                    NumeroRenglon = 1,
                                    CodigoArticulo = "Art01",
                                    DescripcionArticulo = "SoyElArticulo1",
                                    PrecioUnitario = 1,
                                    Cantidad = 10,
                                    Total = 10
                                }
                            ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<NumeracionInvalidaException>(procesadorFacturas.Validar, "Numeración inválida");
        }

        [TestMethod]
        public void Validar_NumeracionCorrelativa02_PimerFactura2TiraError()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<NumeracionInvalidaException>(procesadorFacturas.Validar, "Numeración inválida");
        }

        [TestMethod]
        public void Validar_OrdenCronologico_FacturasDesordenadas_LanzaExcepcion()
        {
            List<Factura> facturas = new List<Factura>
    {
        new Factura
        {
            Numero = 1,
            Fecha = new DateTime(2024, 01, 02),
            CodigoCliente = "CL001",
            RazonSocial = "Cliente 1",
            Cuil = "20-12345678-1",
            Renglones = new List<RenglonFactura>()
        },
        new Factura
        {
            Numero = 2,
            Fecha = new DateTime(2024, 01, 01),
            CodigoCliente = "CL002",
            RazonSocial = "Cliente 2",
            Cuil = "20-87654321-2",
            Renglones = new List<RenglonFactura>()
        }
    };

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<OrdenCronologicoException>(() => procesadorFacturas.Validar());
        }
        #endregion
        #endregion

        #region Consultas
        #region Consultas TotalFacturado
        [TestMethod]
        public void TotalFacturado00_SinFacturasEsCero()
        {
            List<Factura> facturas = [];

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual(0, procesadorFacturas.TotalFacturado());
        }

        [TestMethod]
        public void TotalFacturado01_UnaFactura_TotalFacturadoEsIgualAlLaunicaFactura()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual(12.1, procesadorFacturas.TotalFacturado());
        }

        [TestMethod]
        public void TotalFacturado02_DosFactura_TotalFacturadoEsIgualALaSuma()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArticulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual(133.1, procesadorFacturas.TotalFacturado());
        }
        #endregion
        #region Consultas ArticuloMasVendido
        [TestMethod]
        public void ArticuloMasVendido_FacturasSinRenglones_LanzaExcepcion()
        {
            List<Factura> facturas = new List<Factura>
    {
        new Factura
        {
            Numero = 1,
            Renglones = new List<RenglonFactura>(),
            CodigoCliente = "CL001",
            RazonSocial = "Cliente 1",
            Cuil = "20-12345678-1",
            Fecha = DateTime.Now
        },
        new Factura
        {
            Numero = 2,
            Renglones = new List<RenglonFactura>(),
            CodigoCliente = "CL002",
            RazonSocial = "Cliente 2",
            Cuil = "20-87654321-2",
            Fecha = DateTime.Now
        }
    };

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<ArticuloNoEncontradoException>(() => procesadorFacturas.ArticuloMasVendido());
        }

        [TestMethod]
        public void ArticuloMasVendido_DevuelveArticuloConMasUnidadesVendidas()
        {
            List<Factura> facturas = new List<Factura>
    {
        new Factura
        {
            Numero = 1,
            Renglones = new List<RenglonFactura>
            {
                new RenglonFactura { CodigoArticulo = "Art01", DescripcionArticulo = "Articulo 1", Cantidad = 5 },
                new RenglonFactura { CodigoArticulo = "Art02", DescripcionArticulo = "Articulo 2", Cantidad = 3 }
            },
            CodigoCliente = "CL001",
            RazonSocial = "Cliente 1",
            Cuil = "20-12345678-1",
            Fecha = DateTime.Now
        },
        new Factura
        {
            Numero = 2,
            Renglones = new List<RenglonFactura>
            {
                new RenglonFactura { CodigoArticulo = "Art01", DescripcionArticulo = "Articulo 1", Cantidad = 2 },
                new RenglonFactura { CodigoArticulo = "Art02", DescripcionArticulo = "Articulo 2", Cantidad = 4 }
            },
            CodigoCliente = "CL002",
            RazonSocial = "Cliente 2",
            Cuil = "20-87654321-2",
            Fecha = DateTime.Now
        }
    };

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            string articuloMasVendido = procesadorFacturas.ArticuloMasVendido();

            Assert.AreEqual("Art01", articuloMasVendido);
        }

        [TestMethod]
        public void ArticuloMasVendido_DosArticulosConMismasCantidades_DevuelveUnoDeEllos()
        {
            List<Factura> facturas = new List<Factura>
    {
        new Factura
        {
            Numero = 1,
            Renglones = new List<RenglonFactura>
            {
                new RenglonFactura { CodigoArticulo = "Art01", DescripcionArticulo = "Articulo 1", Cantidad = 5 },
                new RenglonFactura { CodigoArticulo = "Art02", DescripcionArticulo = "Articulo 2", Cantidad = 5 }
            },
            CodigoCliente = "CL001",
            RazonSocial = "Cliente 1",
            Cuil = "20-12345678-1",
            Fecha = DateTime.Now
        }
    };

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            string articuloMasVendido = procesadorFacturas.ArticuloMasVendido();

            Assert.IsTrue(articuloMasVendido == "Art01" || articuloMasVendido == "Art02");
        }
        [TestMethod]
        [ExpectedException(typeof(ArticuloNoEncontradoException))]
        public void ArticuloMasVendido_NoHayArticulos_LanzaArticuloNoEncontradoException()
        {
            List<Factura> facturas = new List<Factura>();

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);

            procesadorFacturas.ArticuloMasVendido();
        }

        #endregion
        #endregion
    }
}