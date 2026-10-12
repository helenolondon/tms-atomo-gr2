using AtmService.ODT;
using AtmServiceDominio.ObjetosDeDominio.Arquivos;
using AtmServiceDominio.ODN;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace AtmService.Arquivos
{
    public class ArquivosService : AtmService, IArquivosService
    {
        private readonly IArquivosOD arquivosOD = null;

        public ArquivosService(IArquivosOD arquivosOD)
        {
            this.arquivosOD = arquivosOD;
        }

        public ArquivoODT ObterArquivo(int codArquivo, bool somenteMetadados)
        {
            ArquivoODT arquivoODT = null;

            var arquivoODN = this.arquivosOD.Obter(codArquivo, false);

            if (arquivoODN != null)
            {
                try
                {
                    arquivoODT = new ArquivoODT(arquivoODN);
                }
                finally
                {
                    if(arquivoODN.File != null)
                    {
                        arquivoODN.File.Dispose();
                    };
                };
            };

            return arquivoODT;
        }

        public void Excluir(int codArquivo)
        {
            this.ExecutarAcaoDeDominio(() => 
            {
                this.arquivosOD.Excluir(codArquivo);
            });
        }

        public void SalvarArquivos(IList<IFormFile> files, string tag, string reference)
        {
            foreach(var file in files)
            {
                var arquivoODT = new ArquivoODT(tag, reference, file);

                this.arquivosOD.Salvar(arquivoODT.ToODN(file));
            };
        }

        public void SalvarArquivo(IFormFile file, string tag, string reference)
        {
            var arquivoODT = new ArquivoODT(tag, reference, file);
            this.arquivosOD.Salvar(arquivoODT.ToODN(file));
        }
    }
}
