using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.ODP;
using AtmServiceInfra.ServicoCategorias;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.ServicoCategorias
{
    public class ServicoCategoriasOD : AtmObjetoDominio<ServicoCategoriaODP>, IServicoCategoriasOD
    {
        public ServicoCategoriasOD(IServicoCategoriasRepositorio repositorio) : base(repositorio)
        {
        }

        public List<ServicoCategoriaODN> Listar()
        {
            return (this.repositorio as IServicoCategoriasRepositorio)
                .Listar()
                .Select(item => new ServicoCategoriaODN
                {
                    CodServicoCategoria = item.CodServicoCategoria,
                    Nome = item.Nome,
                    Descricao = item.Descricao
                })
                .ToList();
        }

        public ServicoCategoriaODN ObterPorId(int codServicoCategoria)
        {
            var odp = this.repositorio.Obter(codServicoCategoria);
            return odp == null ? null : new ServicoCategoriaODN
            {
                CodServicoCategoria = odp.CodServicoCategoria,
                Nome = odp.Nome,
                Descricao = odp.Descricao
            };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var item = obj as ServicoCategoriaODN;
            return item != null && item.CodServicoCategoria > 0 ? new object[] { item.CodServicoCategoria } : null;
        }

        protected internal override void AntesDeSalvar(ServicoCategoriaODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodServicoCategoria = this.repositorio.ObterProximoValorDeSequencia("TD_SERVICOCATEGORIA_SEQ");
            }
        }
    }
}
