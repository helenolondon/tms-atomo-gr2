using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Cidades
{
    public interface ICidadesOD: IAtmObjetoDominio
    {
        CidadeODN ObterCidadePorId(int codCidade);
        List<CidadeODN> ObterCidadesPorEstado(int codEstado);
        List<CidadeODN> ObterCidades(string nomeCidade, int? codigoEstado);
    }
}
