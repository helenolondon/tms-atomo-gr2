using AtmServiceDominio.ODN;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra;
using AtmServiceInfra.Marcas;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.Marcas
{
    public class MarcasOD : AtmObjetoDominio<MarcaODP>, IMarcasOD
    {
        public MarcasOD(IMarcasRepositorio repositorio) : base(repositorio)
        {
        }

        public List<MarcaODN> Listar()
        {
            return (this.repositorio as IMarcasRepositorio)
                .Listar()
                .Select(marca => new MarcaODN { CodMarca = marca.CodMarca, Nome = marca.Nome, Descricao = marca.Descricao, Observacao = marca.Observacao })
                .ToList();
        }

        public MarcaODN ObterPorId(int codMarca)
        {
            var odp = this.repositorio.Obter(codMarca);
            return odp == null ? null : new MarcaODN { CodMarca = odp.CodMarca, Nome = odp.Nome, Descricao = odp.Descricao, Observacao = odp.Observacao };
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var marca = obj as MarcaODN;
            return marca != null && marca.CodMarca > 0 ? new object[] { marca.CodMarca } : null;
        }

        protected internal override void AntesDeSalvar(MarcaODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodMarca = this.repositorio.ObterProximoValorDeSequencia("TD_MARCAS_SEQ");
            }
        }
    }
}
