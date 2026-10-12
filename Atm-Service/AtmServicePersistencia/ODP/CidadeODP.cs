using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TD_CIDADES")]
    public class CidadeODP: ObjetoDePersistencia
    {
        [Key]
        public int CodigoCidade { get; set; }
        [Required]
        public int CodigoEstado { get; set; }
        [Required]
        public string NomeCidade { get; set; }
    }
}
