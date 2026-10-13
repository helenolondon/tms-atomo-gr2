using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.TipoVeiculos;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.TipoVeiculos
{
    public class TipoVeiculosService : AtmService, ITipoVeiculosService
    {
        private readonly ITipoVeiculosOD tipoVeiculosOD;

        public TipoVeiculosService(ITipoVeiculosOD tipoVeiculosOD)
        {
            this.tipoVeiculosOD = tipoVeiculosOD;
        }

        public List<TipoVeiculoODT> Listar()
        {
            return this.tipoVeiculosOD.Listar().Select(item => new TipoVeiculoODT(item)).ToList();
        }

        public TipoVeiculoODT ObterPorId(int codTipoVeiculo)
        {
            TipoVeiculoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.tipoVeiculosOD.ObterPorId(codTipoVeiculo);
                if (item != null)
                {
                    resultado = new TipoVeiculoODT(item);
                }
            });
            return resultado;
        }

        public TipoVeiculoODT Salvar(TipoVeiculoODT item)
        {
            TipoVeiculoODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new TipoVeiculoODT(this.tipoVeiculosOD.Salvar(item.ToODN()) as TipoVeiculoODN);
            });
            return resultado;
        }

        public void Excluir(int codTipoVeiculo)
        {
            this.ExecutarAcaoDeDominio(() => this.tipoVeiculosOD.Excluir(codTipoVeiculo));
        }
    }
}
