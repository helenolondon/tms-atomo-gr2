using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_ARQUIVOS")]
    public class ArquivoODP: ObjetoDePersistencia
    {
        [Key]
        public int CodArquivo { get; set; }
        public string Tag { get; set; }
        public string Reference { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoArquivo { get; set; }
        public string Path { get; set; }
        public long Tamanho { get; set; }
        public DateTime DataArquivo { get; set; }

        [NotMapped]
        public MemoryStream fileStream { get; set; }

        public ArquivoODP()
        {
            this.fileStream = new MemoryStream();
        }
    }
}
