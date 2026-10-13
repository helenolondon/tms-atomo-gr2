using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class ProdutoTipoODN : ObjetoDeNegocio
    {
        public int CodProdutoTipo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as ProdutoTipoODP ?? new ProdutoTipoODP();
            odp.CodProdutoTipo = this.CodProdutoTipo;
            odp.Nome = this.Nome;
            odp.Descricao = this.Descricao;
            return base.ToODP(odp);
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if (obj == null)
            {
                return;
            }

            var odp = obj as ProdutoTipoODP;
            this.CodProdutoTipo = odp.CodProdutoTipo;
            this.Nome = odp.Nome;
            this.Descricao = odp.Descricao;
            base.FromODP(odp);
        }
    }
}
