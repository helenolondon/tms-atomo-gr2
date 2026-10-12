using AtmServiceInfra.EF;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceInfra.Empresas
{
    public class EmpresasRepositorio: Repositorio<EmpresaODP>, IEmpresasRepositorio
    {
        public EmpresasRepositorio(AtmDbContext context): base(context)
        {

        }

        public List<EmpresaODP> ObterEmpresas(int? codEmpresa = null, string razaoSocial = null, string nomeFantasia = null, string cnpj = null, string responsavel = null, 
            string inscricaoEstadual = null, string telefone = null, string email = null)
        {
            var odps = this.context.Empresas
                .Where
                (e => 
                    (codEmpresa == null || e.CodEmpresa == codEmpresa) &&
                    (string.IsNullOrEmpty(razaoSocial) || e.RazaoSocial.ToUpper().Contains(razaoSocial.ToUpper())) &&
                    (string.IsNullOrEmpty(nomeFantasia) || e.NomeFantasia.ToUpper().Contains(nomeFantasia.ToUpper())) &&
                    (string.IsNullOrEmpty(cnpj) || e.Cnpj.ToUpper().Contains(cnpj.ToUpper())) &&
                    (string.IsNullOrEmpty(responsavel) || e.Responsavel.ToUpper().Contains(responsavel.ToUpper())) &&
                    (string.IsNullOrEmpty(inscricaoEstadual) || e.InscricaoEstadual.ToUpper().Contains(inscricaoEstadual.ToUpper())) &&
                    (string.IsNullOrEmpty(telefone) || e.Telefone.ToUpper().Contains(telefone.ToUpper())) &&
                    (string.IsNullOrEmpty(email) || e.Email.ToUpper().Contains(email.ToUpper()))
                 ).OrderBy(e => e.RazaoSocial)!.ToList();

            if(odps != null && odps.Count > 0)
            {
                return odps;
            };

            return null;
        }
    }
}
