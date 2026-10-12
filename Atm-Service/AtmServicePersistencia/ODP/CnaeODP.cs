using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_CNAE")]
    public class CnaeODP: ObjetoDePersistencia
    {
        [Key]
        public int CodigoCnae { get; set; }
        [Required]
        public string DescricaoCnae { get; set; }
    }
}
