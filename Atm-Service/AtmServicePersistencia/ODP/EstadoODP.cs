using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_ESTADOS")]
    public class EstadoODP: ObjetoDePersistencia
    {
        [Key]
        public int CodigoEstado { get; set; }
        [Required]
        public string NomeEstado { get; set; }
        [Required]
        public string UF { get; set; }
    }
}
