using AtmServiceInfra.ODP;
using System.Collections.Generic;

namespace AtmServiceInfra.Empresas
{
    public interface IEmpresasRepositorio: IRepositorio<EmpresaODP>
    {
        public List<EmpresaODP> ObterEmpresas(int? codEmpresa = null, string razaoSocial = null, string nomeFantasia = null, string cnpj = null, 
            string responsavel = null, string inscricaoEstadual = null, string telefone = null, string email = null);
    }
}
