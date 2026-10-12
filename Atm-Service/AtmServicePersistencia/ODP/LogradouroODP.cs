using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_LOGRADOUROS")]
    public class LogradouroODP: ObjetoDePersistencia
    {
        [Key]
        public int CodigoLogradouro { get; set; }
        [Required]
        public int CodigoBairro { get; set; }
        [Required]
        [MaxLength(300)]
        public string NomeLogradouro { get; set; }
        [Required]
        [MaxLength(9)]
        public string Cep { get; set; }
        [Required]
        [MaxLength(30)]
        public string Tipo { get; set; }
    }
}
