using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class ProdutoTipoODT
    {
        public int CodProdutoTipo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ProdutoTipoODT()
        {
        }

        public ProdutoTipoODT(ProdutoTipoODN item)
        {
            FromODN(item);
        }

        public void FromODN(ProdutoTipoODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodProdutoTipo = item.CodProdutoTipo;
            this.Nome = item.Nome;
            this.Descricao = item.Descricao;
        }

        public ProdutoTipoODN ToODN()
        {
            return new ProdutoTipoODN
            {
                CodProdutoTipo = this.CodProdutoTipo,
                Nome = this.Nome,
                Descricao = this.Descricao
            };
        }
    }
}
