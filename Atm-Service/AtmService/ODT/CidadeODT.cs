using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class CidadeODT
    {
        public static CidadeODT NewFromODN(CidadeODN cidadeODN)
        {
            return new CidadeODT()
            {
                CodigoCidade = cidadeODN.CodigoCidade,
                NomeCidade = cidadeODN.NomeCidade
            };
        }

        public int CodigoCidade { get; set; }
        public string NomeCidade { get; set; }
    }
}
