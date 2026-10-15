using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.ServicoCategorias;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.ServicoCategorias
{
    public class ServicoCategoriasService : AtmService, IServicoCategoriasService
    {
        private readonly IServicoCategoriasOD servicoCategoriasOD;

        public ServicoCategoriasService(IServicoCategoriasOD servicoCategoriasOD)
        {
            this.servicoCategoriasOD = servicoCategoriasOD;
        }

        public List<ServicoCategoriaODT> Listar()
        {
            return this.servicoCategoriasOD.Listar().Select(item => new ServicoCategoriaODT(item)).ToList();
        }

        public ServicoCategoriaODT ObterPorId(int codServicoCategoria)
        {
            ServicoCategoriaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var item = this.servicoCategoriasOD.ObterPorId(codServicoCategoria);
                if (item != null)
                {
                    resultado = new ServicoCategoriaODT(item);
                }
            });
            return resultado;
        }

        public ServicoCategoriaODT Salvar(ServicoCategoriaODT item)
        {
            ServicoCategoriaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new ServicoCategoriaODT(this.servicoCategoriasOD.Salvar(item.ToODN()) as ServicoCategoriaODN);
            });
            return resultado;
        }

        public void Excluir(int codServicoCategoria)
        {
            this.ExecutarAcaoDeDominio(() => this.servicoCategoriasOD.Excluir(codServicoCategoria));
        }
    }
}
