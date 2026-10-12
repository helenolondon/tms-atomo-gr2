using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TS_PAISES")]
    public class PaisODP: ObjetoDePersistencia
    {
        [Key]
        public int CodigoPais { get; set; }
        public string SiglaPais { get; set; }
        public string NomePais { get; set; }
    }
}
