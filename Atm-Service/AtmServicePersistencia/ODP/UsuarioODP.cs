using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtmServiceInfra.ODP
{
    /// <summary>
    /// Reqpresenta um usuário do sistema
    /// </summary>
    [Table("TD_USUARIOS")]
    public class UsuarioODP: ObjetoDePersistencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }
    }
}
