using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_BAIRROS")]
    public class BairroODP: ObjetoDePersistencia
    {
        [Key]
        public int CodigoBairro { get; set; }
        [Required]
        public int CodigoCidade { get; set; }
        [Required]
        public string NomeBairro { get; set; }
    }
}
