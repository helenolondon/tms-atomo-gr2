using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    /// <summary>
    /// Representação de contatos no sistema
    /// </summary>
    [Table("TD_CONTATOS")]
    public class ContatoODP: ObjetoDePersistencia
    {
        [Key]
        public int CodContato { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Url { get; set; }
        [Required]
        public string Telefone { get; set; }
        public string Observacoes { get; set; }
    }
}
