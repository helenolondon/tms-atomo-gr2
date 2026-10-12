using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceInfra;
using AtmServiceInfra.Arquivos;
using AtmServiceInfra.ODP;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtmServiceDominio.ObjetosDeDominio.Arquivos
{
    public class ArquivosOD : AtmObjetoDominio<ArquivoODP>, IArquivosOD
    {
        public ArquivosOD(IRepositorio<ArquivoODP> repositorio) : base(repositorio)
        {
        }

        public List<ArquivoODN> ObterArquivos(string reference, string tag)
        {
            var repositorio = this.repositorio as IArquivosRepositorio;
            var arquivosODP = repositorio.ObterArquivos(tag, reference);

            if(arquivosODP != null && arquivosODP.Count > 0)
            {
                return arquivosODP.Select(odp => new ArquivoODN(odp)).ToList();
            };

            return null;
        }

        public ArquivoODN Obter(int codArquivo, bool somenteMetadados = true)
        {
            ArquivoODN arquivo = null;
            var repositorio = this.repositorio as IArquivosRepositorio;

            var arquivoODP = this.repositorio.Obter(codArquivo);

            if(arquivoODP != null)
            {
                arquivo = new ArquivoODN(arquivoODP);

                if (!somenteMetadados)
                {
                    using(var stream = repositorio.ObterArquivoStream(codArquivo))
                    {
                        if (stream != null && stream.Length > 0)
                        {
                            stream.CopyTo(arquivo.File);
                        };
                    };
                };
            };

            return arquivo;
        }

        protected internal override void AntesDeSalvar(ArquivoODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodArquivo = this.repositorio.ObterProximoValorDeSequencia("TD_ARQUIVOS_SEQ");
            }
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var temp = obj as ArquivoODN;

            if (temp.CodArquivo <= 0)
            {
                return null;
            }

            return new object[] { temp.CodArquivo };
        }
    }
}
