using AtmServiceDominio.ObjetosDeDominio.Validacao;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class ArquivoODN: ObjetoDeNegocio
    {
        public int CodArquivo { get; set; }
        public string Tag { get; set; }
        public string Reference { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoArquivo { get; set; }
        public long Tamanho { get; set; }
        public MemoryStream File { get; set; }
        public DateTime DataCadastro { get; internal set; }

        public ArquivoODN()
        {
            this.File = new MemoryStream();
        }

        public ArquivoODN(ArquivoODP arquivoODP): this()
        {
            this.FromODP(arquivoODP);
        }

        public ArquivoODN(MemoryStream file, string tag, string reference)
        {
            this.File = file;
            this.Tag = tag;
            this.Reference = reference;
        }

        protected internal override void ConfiguraRegrasDeValidacao()
        {
            this.regrasDeValidacao.Add(new ObjetosDeDominio.Validacao.TamanhoMaximoRV(this, "Tag", 100, "Tag do arquivo deve conter no máximo 1000 caracteres"));
            this.regrasDeValidacao.Add(new ObjetosDeDominio.Validacao.TamanhoMaximoRV(this, "Reference", 100, "Reference do arquivo deve conter no máximo 1000 caracteres"));
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            if(obj == null)
            {
                return;
            };

            var odp = obj as ArquivoODP;

            this.CodArquivo = odp.CodArquivo;
            this.NomeArquivo = odp.NomeArquivo;
            this.Reference = odp.Reference;
            this.Tag = odp.Tag;
            this.Tamanho = odp.Tamanho;
            this.TipoArquivo = odp.TipoArquivo;
            this.DataCadastro = odp.DataCad;
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            if (destino == null)
            {
                destino = new ArquivoODP();
            };

            var odp = destino as ArquivoODP;

            odp.CodArquivo = this.CodArquivo;
            odp.NomeArquivo = this.NomeArquivo;
            odp.Reference = this.Reference;
            odp.Tag = this.Tag;
            odp.Tamanho = this.Tamanho;
            odp.TipoArquivo = this.TipoArquivo;

            if(this.File.Length > 0)
            {
                this.File.Position = 0;
                this.File.CopyTo(odp.fileStream);
            }

            return odp;
        }
    }
}
