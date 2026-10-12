using AtmServiceDominio.ObjetosDeDominio.Validacao;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;

namespace AtmServiceDominio.ODN
{
    public class UsuarioODN: ObjetoDeNegocio
    {
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }

        protected internal override void ConfiguraRegrasDeValidacao()
        {            
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Nome", 100, "Nome deve ter no máximo 100 letras"));
            this.regrasDeValidacao.Add(new TamanhoMinimoRV(this, "Nome", 2, "Nome deve ter no mínimo 2 letras"));

            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Telefone", 40, "Telefone deve ter no máximo 40 letras/números"));

            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Nome", "Nome é de preenchimento obrigatório"));
            this.regrasDeValidacao.Add(new ObrigatorioRV(this, "Login", "Login é de preenchimento obrigatório"));
        }

        internal override void FromODP(ObjetoDePersistencia origem)
        {
            var odp = origem as UsuarioODP;

            this.Email = odp.Email;
            this.Nome = odp.Nome;
            this.CodUsuario = odp.CodUsuario;
            this.Login = odp.Login;
            this.Telefone = odp.Telefone;
            this.DataCadastro = odp.DataCad;
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino)
        {
            var odp = destino as UsuarioODP;

            if (odp == null)
            {
                odp = new UsuarioODP();
            }

            odp.Email = this.Email;
            odp.CodUsuario = this.CodUsuario;
            odp.Login = this.Login;
            odp.Nome = this.Nome;
            odp.Telefone = this.Telefone;

            return odp;
        }
    }
}
