using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmServiceInfra.Clientes.PrecosTarefas
{
    public class ClientesPrecosTarefasRepositorio : Repositorio<ClientePrecoTarefaODP>, IClientesPrecosTarefasRepositorio
    {
        public ClientesPrecosTarefasRepositorio(AtmDbContext context): base(context)
        {

        }

        public List<ClientePrecoTarefaODP> ObterPrecosPorCliente(int codCliente)
        {
            var odps = this.context.ClientesPrecosTarefas.Where(n => n.CodCliente == codCliente)
            .OrderBy(n => n.CodClientePrecoTarefa)
            .ToList();

            if (odps != null && odps.Count > 0)
            {
                odps.OrderBy(t => t.CodClientePrecoTarefa);

                return odps;
            }

            return null;
        }
    }
}
