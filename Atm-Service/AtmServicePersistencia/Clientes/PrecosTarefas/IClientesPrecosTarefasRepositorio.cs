using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Clientes.PrecosTarefas
{
    public interface IClientesPrecosTarefasRepositorio : IRepositorio<ClientePrecoTarefaODP>
    {
        public List<ClientePrecoTarefaODP> ObterPrecosPorCliente(int codCliente);
    }
}
