using AtmServiceDominio.ODN;
using AtmServiceInfra;
using AtmServiceInfra.Bairros;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Bairros
{
    public class BairrosOD : AtmObjetoDominio<BairroODP>, IBairrosOD
    {
        public BairrosOD(IRepositorio<BairroODP> repositorio) : base(repositorio)
        {

        }

        public List<BairroODN> ObterBairroPorCidade(int codigoCidade)
        {
            var q = (this.repositorio as BairrosRepositorio)
                .ObterBairroPorCidade(codigoCidade);

            List<BairroODN> ret = new List<BairroODN>();

            if (q == null || q.Count == 0)
            {
                return null;
            }

            var odps = q.ToList();

            foreach(BairroODP item in odps)
            {
                var odn = new BairroODN();
                odn.FromODP(item);

                ret.Add(odn);
            }
            
            return ret;
        }

        public BairroODN ObterBairroPorId(int codigoBairro)
        {
            var odp = this.repositorio.Obter(codigoBairro);

            if(odp == null)
            {
                return null;
            }

            var odn = new BairroODN();
            odn.FromODP(odp);

            return odn;
        }
    }
}
