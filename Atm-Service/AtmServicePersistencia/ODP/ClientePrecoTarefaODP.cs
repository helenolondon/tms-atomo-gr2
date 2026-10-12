using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    [Table("TL_CLIENTESPRECOSTAREFAS")]
    public class ClientePrecoTarefaODP: ObjetoDePersistencia
    {
        [Key]
        public int CodClientePrecoTarefa { get; set; }
        public int CodCliente { get; set; }
        public int CodTarefa { get; set; }
        public decimal Preco { get; set; }
    }
}
