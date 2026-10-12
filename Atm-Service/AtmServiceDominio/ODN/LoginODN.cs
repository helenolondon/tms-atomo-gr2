using AtmServiceDominio.ObjetosDeDominio.Validacao;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ODN
{
    public class LoginODN: ObjetoDeNegocio
    {
        public int CodLogin { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        protected internal override void ConfiguraRegrasDeValidacao()
        {
            base.ConfiguraRegrasDeValidacao();

            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Login", 60, $"Login deve ter no máximo 60 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMinimoRV(this, "Login", 4, $"Login deve ter no mínimo 4 caracteres"));

            this.regrasDeValidacao.Add(new TamanhoMinimoRV(this, "Senha", 6, $"Senha deve ter no mínimo 6 caracteres"));
            this.regrasDeValidacao.Add(new TamanhoMaximoRV(this, "Senha", 50, $"Senha deve ter no máximo 50 caracteres"));
        }

        internal override ObjetoDePersistencia ToODP(ObjetoDePersistencia destino = null)
        {
            var d = destino as LoginODP;

            if (destino == null)
            {
                d = new LoginODP(); 
            }

            d.CodLogin = this.CodLogin;
            d.Login = this.Login;
            d.Senha = this.Senha;

            return d;
        }

        internal LoginODP ToLoginODP(LoginODP destino = null)
        {
            return this.ToODP(destino) as LoginODP;
        }

        internal override void FromODP(ObjetoDePersistencia obj)
        {
            var odp = obj as LoginODP;

            this.Senha = odp.Senha;
            this.CodLogin = odp.CodLogin;
            this.Login = odp.Login;
        }

        public static LoginODN NewFromLoginODP(LoginODP login)
        {
            var odn = new LoginODN();

            odn.FromODP(login);

            return odn;
        }
    }
}
