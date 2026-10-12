using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Cidades
{
    public interface ICidadesRepositorio: IRepositorio<CidadeODP>
    {
        List<CidadeODP> ObterCidadesPorEstado(int codEstado);
        List<CidadeODP> ObterCidades(string nomeCidade, int? codigoEstado = null);
    }
}
