using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class ProdutoGrupoODT
    {
        public int CodProdutoGrupo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ProdutoGrupoODT()
        {
        }

        public ProdutoGrupoODT(ProdutoGrupoODN item)
        {
            FromODN(item);
        }

        public void FromODN(ProdutoGrupoODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodProdutoGrupo = item.CodProdutoGrupo;
            this.Nome = item.Nome;
            this.Descricao = item.Descricao;
        }

        public ProdutoGrupoODN ToODN()
        {
            return new ProdutoGrupoODN
            {
                CodProdutoGrupo = this.CodProdutoGrupo,
                Nome = this.Nome,
                Descricao = this.Descricao
            };
        }
    }
}
