using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Cnaes;
using AtmServiceDominio.ObjetosDeDominio.Empresas;
using AtmServiceDominio.ODN;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AtmService.Empresas
{
    public class EmpresasService: AtmService, IEmpresasService
    {
        private readonly IEmpresasOD empresasOD = null;
        private readonly ICnaesOD cnaesOD = null;

        public EmpresasService(IEmpresasOD empresasOD, ICnaesOD cnaesOD) 
        {
            this.empresasOD = empresasOD;
            this.cnaesOD = cnaesOD;
        }
        
        public List<EmpresaODT> ListarEmpresas(string razaoSocial = null, string nomeFantasia = null, string cnpj = null, string responsavel = null, string inscricaoEstadual = null, 
            string telefone = null, string email = null)
        {
            var odns = this.empresasOD.ListarEmpresas(razaoSocial, nomeFantasia, cnpj, responsavel, inscricaoEstadual, telefone, email);

            if(odns != null)
            {
                return odns.Select(odn => new EmpresaODT(odn)).ToList();
            };

            return new List<EmpresaODT>();
        }

        public EmpresaODT ObterEmpresa(int codEmpresa)
        {
            EmpresaODT result = null;

            this.ExecutarAcaoDeDominio(() => 
            {
                var odn = this.empresasOD.ObterEmpresa(codEmpresa);

                if (odn != null)
                {
                    result = this.Expand(new EmpresaODT(odn));
                };
            });

            return result;
        }

        public EmpresaODT SalvarEmpresa(EmpresaODT odt)
        {
            EmpresaODT result = null;

            this.ExecutarAcaoDeDominio(() =>
            {
                result = new EmpresaODT(this.empresasOD.Salvar(odt.ToODN()) as EmpresaODN);
            });

            return result;
        }

        public void Excluir(int codEmpresa)
        {
            this.ExecutarAcaoDeDominio(() => 
            {
                this.empresasOD.Excluir(codEmpresa);
            });
        }

        private EmpresaODT Expand(EmpresaODT odt)
        {
            if(odt == null)
            {
                return null;
            };

            var cnae = this.cnaesOD.ObterCnaePorId(odt.CnaePrincipal);

            if (cnae != null)
            {
                odt.Cnae = new CnaeODT(cnae);
            };

            return odt;
        }
    }
}
