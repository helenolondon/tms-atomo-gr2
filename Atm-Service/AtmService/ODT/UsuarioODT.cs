using AtmServiceDominio.ODN;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmService.ODT
{
    public class UsuarioODT
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Telefone { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public bool Supervisor { get; set; }

        public UsuarioODT()
        {

        }

        public UsuarioODT(UsuarioODN odn)
        {
            this.FromODN(odn);
        }

        internal UsuarioODN ToUsuarioODN()
        {
            var odn = new UsuarioODN();

            odn.Email = this.Email;
            odn.CodUsuario = this.Id;
            odn.Login = this.Login;
            odn.Nome = this.Nome;
            odn.Telefone = this.Telefone;

            return odn;
        }

        internal static UsuarioODT NewFromODN(UsuarioODN origem)
        {
            return new UsuarioODT()
            {
                Id = origem.CodUsuario,
                Email = origem.Email,
                Login = origem.Login,
                Nome = origem.Nome,
                Telefone = origem.Telefone,
                DataDeCadastro = origem.DataCadastro,
            };
        }

        internal void FromODN(UsuarioODN origem)
    {
            this.Id = origem.CodUsuario;
            this.Email = origem.Email;
            this.Login = origem.Login;
            this.Nome = origem.Nome;
            this.Telefone = origem.Telefone;
        }
    }
}
