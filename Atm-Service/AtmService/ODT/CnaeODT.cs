using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class CnaeODT
    {
        public int CodigoCnae { get; set; }
        public string DescricaoCnae { get; set; }
        public string Cnae { get { return this.CodigoCnae.ToString() + " - " + this.DescricaoCnae; } }

        public CnaeODT()
        {

        }

        public CnaeODT(CnaeODN cnae)
        {
            this.FromODN(cnae);
        }

        public void FromODN(CnaeODN cnae)
        {
            if(cnae != null)
            {
                this.CodigoCnae = cnae.CodigoCnae;
                this.DescricaoCnae = cnae.DescricaoCnae;
            }
        }

        public static CnaeODT NewFromODN(CnaeODN cnaeODN)
        {
            return new CnaeODT()
            {
                CodigoCnae = cnaeODN.CodigoCnae,
                DescricaoCnae = cnaeODN.DescricaoCnae
            };
        }
    }
}
