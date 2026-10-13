using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Marcas;
using AtmServiceDominio.ODN;
using System.Collections.Generic;
using System.Linq;

namespace AtmService.Marcas
{
    public class MarcasService : AtmService, IMarcasService
    {
        private readonly IMarcasOD marcasOD;

        public MarcasService(IMarcasOD marcasOD)
        {
            this.marcasOD = marcasOD;
        }

        public List<MarcaODT> Listar()
        {
            return this.marcasOD.Listar().Select(marca => new MarcaODT(marca)).ToList();
        }

        public MarcaODT ObterPorId(int codMarca)
        {
            MarcaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                var marca = this.marcasOD.ObterPorId(codMarca);
                if (marca != null)
                {
                    resultado = new MarcaODT(marca);
                }
            });
            return resultado;
        }

        public MarcaODT Salvar(MarcaODT marca)
        {
            MarcaODT resultado = null;
            this.ExecutarAcaoDeDominio(() =>
            {
                resultado = new MarcaODT(this.marcasOD.Salvar(marca.ToODN()) as MarcaODN);
            });
            return resultado;
        }

        public void Excluir(int codMarca)
        {
            this.ExecutarAcaoDeDominio(() => this.marcasOD.Excluir(codMarca));
        }
    }
}
