using AtmService.ODT;
using System.Collections.Generic;

namespace AtmService.Empresas
{
    public interface IEmpresasService
    {
        List<EmpresaODT> ListarEmpresas(string razaoSocial = null, string nomeFantasia = null, string cnpj = null, string responsavel = null, string inscricaoEstadual = null, 
            string telefone = null, string email = null);
        EmpresaODT ObterEmpresa(int codEmpresa);
        EmpresaODT SalvarEmpresa(EmpresaODT odt);
        void Excluir(int codEmpresa);
    }
}
