using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class ProdutoTipoAgressividadeODT
    {
        public int CodProdutoTipoAgressividade { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ProdutoTipoAgressividadeODT()
        {
        }

        public ProdutoTipoAgressividadeODT(ProdutoTipoAgressividadeODN item)
        {
            FromODN(item);
        }

        public void FromODN(ProdutoTipoAgressividadeODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodProdutoTipoAgressividade = item.CodProdutoTipoAgressividade;
            this.Nome = item.Nome;
            this.Descricao = item.Descricao;
        }

        public ProdutoTipoAgressividadeODN ToODN()
        {
            return new ProdutoTipoAgressividadeODN
            {
                CodProdutoTipoAgressividade = this.CodProdutoTipoAgressividade,
                Nome = this.Nome,
                Descricao = this.Descricao
            };
        }
    }
}
