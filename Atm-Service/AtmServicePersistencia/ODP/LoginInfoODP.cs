using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    /// <summary>
    /// Representação de 1 token de login no sistema
    /// </summary>
    [Table("TD_LOGININFO")]
    public class LoginInfoODP: ObjetoDePersistencia
    {
        [Key]
        public int CodLoginInfo { get; set; }

        [Required]
        public string Login { get; set; }

        public DateTime? DataLogout { get; set; }
        public DateTime? DataLogin { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public int Ativo { get; set; }
    }
}
