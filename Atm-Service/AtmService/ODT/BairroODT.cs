using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class BairroODT
    {
        public int CodigoBairro { get; set; }
        public string NomeBairro { get; set; }

        public static BairroODT NewFromODN(BairroODN bairroODN)
        {
            return new BairroODT()
            {
                CodigoBairro = bairroODN.CodigoBairro,
                NomeBairro = bairroODN.NomeBairro
            };
        }
    }
}
