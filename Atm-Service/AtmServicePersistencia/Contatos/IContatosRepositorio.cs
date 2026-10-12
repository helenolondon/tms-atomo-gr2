using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceInfra.Contatos
{
    public interface IContatosRepositorio: IRepositorio<ContatoODP> 
    {
        List<ContatoODP> ObterContatosPorNome(string nome);
    }
}
