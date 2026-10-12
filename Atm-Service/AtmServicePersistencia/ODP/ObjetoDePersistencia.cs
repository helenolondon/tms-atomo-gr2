using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmServiceInfra.ODP
{
    public class ObjetoDePersistencia
    {
        /// <summary>
        /// Data úiltima alteração
        /// </summary>
        public DateTime? DataAlt { get; set; }

        /// <summary>
        /// Usuário que fez a última alteração
        /// </summary>
        public string UsuarioAlt { get; set; }

        /// <summary>
        /// Data de cadastro
        /// </summary>
        [Required]
        public DateTime DataCad { get; set; }

        /// <summary>
        /// Usuário inseriu no sistema
        /// </summary>
        [Required]
        public string UsuarioCad { get; set; }
    }
}
