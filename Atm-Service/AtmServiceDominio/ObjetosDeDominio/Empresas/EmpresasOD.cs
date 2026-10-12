using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceInfra;
using AtmServiceInfra.Empresas;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.Empresas
{
    public class EmpresasOD: AtmObjetoDominio<EmpresaODP>, IEmpresasOD
    {
        public EmpresasOD(IRepositorio<EmpresaODP> repositorio) : base(repositorio)
        {
        }

        public List<EmpresaODN> ListarEmpresas(string razaoSocial = null, string nomeFantasia = null, string cnpj = null, string responsavel = null, string inscricaoEstadual = null, 
            string telefone = null, string email = null)
        {
            var odps = (this.repositorio as IEmpresasRepositorio).ObterEmpresas(null, razaoSocial, nomeFantasia, cnpj, responsavel, inscricaoEstadual, telefone, email);

            if(odps != null && odps.Count > 0) 
            {
                return odps.Select(e => new EmpresaODN(e)).ToList();
            }

            return  null;
        }

        public EmpresaODN ObterEmpresa(int codEmpresa)
        {
            var odps = (this.repositorio as IEmpresasRepositorio).ObterEmpresas(codEmpresa);

            if(odps != null && odps.Count > 0)
            {
                return new EmpresaODN(odps[0]);
            };

            return null;
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var temp = obj as EmpresaODN;

            if (temp.CodEmpresa <= 0)
            {
                return null;
            }

            return new object[] { temp.CodEmpresa };
        }

        protected internal override void AntesDeSalvar(EmpresaODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodEmpresa = this.repositorio.ObterProximoValorDeSequencia("TD_EMPRESAS_SEQ");
            }
        }
    }
}
