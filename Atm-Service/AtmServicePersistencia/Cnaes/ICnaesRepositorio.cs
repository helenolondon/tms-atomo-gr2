using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Cnaes
{
    public interface ICnaesRepositorio: IRepositorio<CnaeODP> 
    {
        List<CnaeODP> ObterCnaes(string filtro);
        List<CnaeODP> ObterCnaes(int codCnaeInicial, int? codCnaeFinal);
    }
}
