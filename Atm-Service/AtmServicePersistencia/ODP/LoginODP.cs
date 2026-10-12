using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    /// <summary>
    /// Reqpresenta um login do sistema
    /// </summary>
    [Table("TD_LOGIN")]
    public class LoginODP: ObjetoDePersistencia
    {
        [Key]
        public int CodLogin { get; set; }

        public string Login{ get; set; }

        public string Senha { get; set; }

        public UsuarioODP UsuarioCadastro { get; set; }
        public UsuarioODP UsuarioAlteracao { get; set; }
    }
}
