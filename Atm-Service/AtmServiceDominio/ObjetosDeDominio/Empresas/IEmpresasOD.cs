using AtmServiceDominio.ODN;
using System.Collections.Generic;

namespace AtmServiceDominio.ObjetosDeDominio.Empresas
{
    public interface IEmpresasOD : IAtmObjetoDominio
    {
        EmpresaODN ObterEmpresa(int codEmpresa);
        List<EmpresaODN> ListarEmpresas(string razaoSocial = null, string nomeFantasia = null, string cnpj = null, string responsavel = null, string inscricaoEstadual = null,
            string telefone = null, string email = null);
    }
}
