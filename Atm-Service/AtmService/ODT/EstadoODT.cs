using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class EstadoODT
    {
        public static EstadoODT NewFromODN(EstadoODN estadoODN)
        {
            return new EstadoODT()
            {
                CodigoEstado = estadoODN.CodigoEstado,
                NomeEstado = estadoODN.NomeEstado,
                UF = estadoODN.UF
            };
        }

        public int CodigoEstado { get; set; }
        public string NomeEstado { get; set; }
        public string UF { get; set; }
    }
}
