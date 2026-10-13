using AtmServiceDominio.ODN;

namespace AtmService.ODT
{
    public class TipoVeiculoODT
    {
        public int CodTipoVeiculo { get; set; }
        public string Descricao { get; set; }

        public TipoVeiculoODT()
        {
        }

        public TipoVeiculoODT(TipoVeiculoODN item)
        {
            FromODN(item);
        }

        public void FromODN(TipoVeiculoODN item)
        {
            if (item == null)
            {
                return;
            }

            this.CodTipoVeiculo = item.CodTipoVeiculo;
            this.Descricao = item.Descricao;
        }

        public TipoVeiculoODN ToODN()
        {
            return new TipoVeiculoODN
            {
                CodTipoVeiculo = this.CodTipoVeiculo,
                Descricao = this.Descricao
            };
        }
    }
}
