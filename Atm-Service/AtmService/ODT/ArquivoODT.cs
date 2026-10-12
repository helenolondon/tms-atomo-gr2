using AtmServiceDominio.ODN;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AtmService.ODT
{
    public class ArquivoODT
    {
        public int CodArquivo { get; set; }
        public string Tag { get; set; }
        public string Reference { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoArquivo { get; set; }
        public long Tamanho { get; set; }
        public DateTime DataArquivo { get; set; }
        public byte[] FileAsBytes { get; internal set; }

        public ArquivoODT(ArquivoODN arquivoODN)
        {
            this.FromODN(arquivoODN);
        }

        public ArquivoODT(string tag, string reference, IFormFile formFile)
        {
            this.Tag = tag;
            this.Reference = reference;

            this.ReadMetaDataFromFormFile(formFile);
        }

        private void ReadMetaDataFromFormFile(IFormFile formFile)
        {
            this.NomeArquivo = formFile.FileName;
            this.TipoArquivo = formFile.ContentType;
            this.Tamanho = formFile.Length;
        }

        public ArquivoODN ToODN(IFormFile file)
        {
            var odn = new ArquivoODN();

            odn.CodArquivo = this.CodArquivo;
            odn.NomeArquivo = this.NomeArquivo;
            odn.Reference = this.Reference;
            odn.Tag = this.Tag;
            odn.Tamanho = this.Tamanho;
            odn.TipoArquivo = this.TipoArquivo;

            if(file != null)
            {
                odn.File = new MemoryStream();
                file.CopyTo(odn.File);
            }

            return odn;
        }

        public void FromODN(ArquivoODN arquivoODN)
        {
            if(arquivoODN == null)
            {
                return;
            };
            
            this.CodArquivo = arquivoODN.CodArquivo;
            this.NomeArquivo = arquivoODN.NomeArquivo;
            this.Reference = arquivoODN.Reference;
            this.Tag = arquivoODN.Tag;
            this.Tamanho = arquivoODN.Tamanho;
            this.TipoArquivo = arquivoODN.TipoArquivo;
            this.DataArquivo = arquivoODN.DataCadastro;

            if (arquivoODN.File != null)
            {
                this.FileAsBytes = arquivoODN.File.ToArray();
            }
        }
    }
}
