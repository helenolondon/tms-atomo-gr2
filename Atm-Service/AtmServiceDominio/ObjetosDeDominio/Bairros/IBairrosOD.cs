using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Bairros
{
    public interface IBairrosOD: IAtmObjetoDominio
    {
        public BairroODN ObterBairroPorId(int codigoBairro);
        public List<BairroODN> ObterBairroPorCidade(int codigoCidade);
    }
}
