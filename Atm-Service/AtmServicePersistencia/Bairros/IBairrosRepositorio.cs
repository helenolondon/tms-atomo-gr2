using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Bairros
{
    public interface IBairrosRepositorio: IRepositorio<BairroODP>
    {
        List<BairroODP> ObterBairroPorCidade(int codCidade);
    }
}
