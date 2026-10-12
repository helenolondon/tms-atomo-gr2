using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Logradouros
{
    public interface ILogradourosRepositorio: IRepositorio<LogradouroODP>
    {
        LogradouroODP ObterPorCep(string cep);
    }
}
