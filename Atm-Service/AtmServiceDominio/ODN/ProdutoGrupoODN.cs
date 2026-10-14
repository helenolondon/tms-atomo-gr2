using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ODN
{
    public class ProdutoGrupoODN : ObjetoDeNegocio
    {
        public int CodProdutoGrupo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var odp = destino as ProdutoGrupoODP ?? new ProdutoGrupoODP();
            odp.CodProdutoGrupo = this.CodProdutoGrupo;
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

            var odp = obj as ProdutoGrupoODP;
            this.CodProdutoGrupo = odp.CodProdutoGrupo;
            this.Nome = odp.Nome;
            this.Descricao = odp.Descricao;
            base.FromODP(odp);
        }
    }
}
