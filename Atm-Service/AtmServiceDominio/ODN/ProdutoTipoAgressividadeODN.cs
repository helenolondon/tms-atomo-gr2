using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class ProdutoTipoAgressividadeODN : ObjetoDeNegocio
    {
        public int CodProdutoTipoAgressividade { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as ProdutoTipoAgressividadeODP ?? new ProdutoTipoAgressividadeODP();
            odp.CodProdutoTipoAgressividade = this.CodProdutoTipoAgressividade;
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

            var odp = obj as ProdutoTipoAgressividadeODP;
            this.CodProdutoTipoAgressividade = odp.CodProdutoTipoAgressividade;
            this.Nome = odp.Nome;
            this.Descricao = odp.Descricao;
            base.FromODP(odp);
        }
    }
}
