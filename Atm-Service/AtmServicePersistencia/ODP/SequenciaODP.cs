using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    /// <summary>
    /// Representação de uma sequência do banco de dados
    /// </summary>
    [Table("TS_SEQUENCIAS")]
    public class SequenciaODP
    {
        [Key]
        public string NomeSequencia { get; set; }

        [Required]
        public int UltimaSequencia { get; set; }
    }
}
