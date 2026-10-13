using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class UnidadeDeMedidaODT
    {
        public int CodUnidadeDeMedida { get; set; }
        public string Nome { get; set; }
        public string Unidade { get; set; }

        public UnidadeDeMedidaODT()
        {
        }

        public UnidadeDeMedidaODT(UnidadeDeMedidaODN item)
        {
            FromODN(item);
        }

        public void FromODN(UnidadeDeMedidaODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodUnidadeDeMedida = item.CodUnidadeDeMedida;
            this.Nome = item.Nome;
            this.Unidade = item.Unidade;
        }

        public UnidadeDeMedidaODN ToODN()
        {
            return new UnidadeDeMedidaODN
            {
                CodUnidadeDeMedida = this.CodUnidadeDeMedida,
                Nome = this.Nome,
                Unidade = this.Unidade
            };
        }
    }
}
