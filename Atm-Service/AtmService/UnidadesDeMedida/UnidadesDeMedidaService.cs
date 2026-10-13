using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.UnidadesDeMedida;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.UnidadesDeMedida
{
    public class UnidadesDeMedidaService : AtmService, IUnidadesDeMedidaService
    {
        private readonly IUnidadesDeMedidaOD unidadesDeMedidaOD;

        public UnidadesDeMedidaService(IUnidadesDeMedidaOD unidadesDeMedidaOD)
        {
            this.unidadesDeMedidaOD = unidadesDeMedidaOD;
        }

        public List<UnidadeDeMedidaODT> Listar()
        {
            return this.unidadesDeMedidaOD.Listar().Select(item => new UnidadeDeMedidaODT(item)).ToList();
        }

        public UnidadeDeMedidaODT ObterPorId(int codUnidadeDeMedida)
        {
            UnidadeDeMedidaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.unidadesDeMedidaOD.ObterPorId(codUnidadeDeMedida);
                if (item != null)
                {
                    resultado = new UnidadeDeMedidaODT(item);
                }
            });
            return resultado;
        }

        public UnidadeDeMedidaODT Salvar(UnidadeDeMedidaODT item)
        {
            UnidadeDeMedidaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new UnidadeDeMedidaODT(this.unidadesDeMedidaOD.Salvar(item.ToODN()) as UnidadeDeMedidaODN);
            });
            return resultado;
        }

        public void Excluir(int codUnidadeDeMedida)
        {
            this.ExecutarAcaoDeDominio(() => this.unidadesDeMedidaOD.Excluir(codUnidadeDeMedida));
        }
    }
}
