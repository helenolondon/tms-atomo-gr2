using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeDominio.Seguranca;
using AtmServiceDominio.ObjetosDeDominio.Seguranca.Erros;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceInfra.ODP;
using AtmServiceInfra.Seguranca;
using System;
using System.Security.Cryptography;

namespace AtmServiceDominio.ObjetosDeDominio
{
    public class SegurancaOD: AtmObjetoDominio<LoginODP>, ISegurancaOD
    {
        private readonly ISegurancaRepositorio segurancaRep;
        private readonly ILoginInfoRepositorio loginInfoRep;

        public SegurancaOD(ISegurancaRepositorio segurancaRep, ILoginInfoRepositorio loginInfoRep): base(segurancaRep)
        {
            this.segurancaRep = segurancaRep;
            this.loginInfoRep = loginInfoRep;
        }

        public string GerarSenhaSegura()
        {
            return this.GeneratePassword(8);
        }

        private string GeneratePassword(int len)
        {
            bool requireNonLetterOrDigit = true;
            bool requireDigit = true;
            bool requireLowercase = true;
            bool requireUppercase = true;

            string randomPassword = string.Empty;

            int passwordLength = len;

            Random random = new Random();
            while (randomPassword.Length != passwordLength)
            {
                int randomNumber = random.Next(48, 122);  // >= 48 && < 122 
                if (randomNumber == 95 || randomNumber == 96) continue;  // != 95, 96 _'

                char c = Convert.ToChar(randomNumber);

                if (requireDigit)
                    if (char.IsDigit(c))
                        requireDigit = false;

                if (requireLowercase)
                    if (char.IsLower(c))
                        requireLowercase = false;

                if (requireUppercase)
                    if (char.IsUpper(c))
                        requireUppercase = false;

                if (requireNonLetterOrDigit)
                    if (!char.IsLetterOrDigit(c))
                        requireNonLetterOrDigit = false;

                randomPassword += c;
            }

            if (requireDigit)
                randomPassword += Convert.ToChar(random.Next(48, 58));  // 0-9

            if (requireLowercase)
                randomPassword += Convert.ToChar(random.Next(97, 123));  // a-z

            if (requireUppercase)
                randomPassword += Convert.ToChar(random.Next(65, 91));  // A-Z

            if (requireNonLetterOrDigit)
                randomPassword += Convert.ToChar(random.Next(33, 48));  // symbols !"#$%&'()*+,-./

            return randomPassword;
        }

        public string LoginDeToken(string token)
        {
            var c = new AtmCriptografia();

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var ret = c.Decrypt(token);
            ret = ret.Substring(0, ret.IndexOf("/;"));

            return string.IsNullOrEmpty(ret) ? null : ret;
        }

        public string Criptografar(string senha)
        {
            var c = new AtmCriptografia();

            c.ValueToCrypt = senha;

            return c.ValueCrypted;
        }

        public string DesCriptografar(string senha)
        {
            return new AtmCriptografia().Decrypt(senha);
        }

        public LoginODN SalvarLogin(LoginODN login)
        {
            return this.Salvar(login) as LoginODN;            
        }

        public LoginODN ObterLoginPorLogin(string login)
        {
            var loginODP = this.segurancaRep.ObterLoginPorLogin(login);

            if (loginODP != null)
            {
                return LoginODN.NewFromLoginODP(loginODP);
            }

            return null;
        }

        public void AlteraSenhaUsuario(string senha, LoginODN login)
        {
            try
            {
                login.Senha = senha;
                this.Salvar(login);
            }
            catch(EstadoDoObjetoDeNegcioEInvalidoDE ex)
            {
                if (ex.ODN[0].ErrosDeValidacao.Count == 1)
                {
                    throw new ErroDeValidacaoDE(ex.ODN[0].ErrosDeValidacao[0]);
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw new DominioExcecao(ex.Message);
            }            
        }

        private string GerarToken(LoginInfoODP loginInfo)
        {
            return this.Criptografar(loginInfo.CodLoginInfo.ToString() + "/;" + loginInfo.Login + "/;" +
                loginInfo.DataCad.ToString() + "/;" + ObterTokenTempoDeVida().ToString()+"/;");
        }

        /// <summary>
        /// Verifica se um token é válido
        /// </summary>
        public bool TokenValido(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                var decript = this.DesCriptografar(token);
                var valoresToken = decript.Split("/;");

                var dataCad = valoresToken[2];
                var tempoVida = valoresToken[3];

                return !(DateTime.Now.Subtract(DateTime.Parse(dataCad)).TotalMinutes > int.Parse(tempoVida));
            }
            catch (CryptographicException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public LoginODN Login(string login, string senha, out string token)
        {
            token = null;

            if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(login))
            {
                throw new LoginInvalidoDE("Login inválido");
            }

            try
            {
                var loginODP = this.segurancaRep.ObterLoginPorLogin(login.ToUpper());

                if (loginODP == null)
                {
                    return null;
                }

                var cript = this.Criptografar(senha);

                if(cript == loginODP.Senha)
                {
                    token = this.GerarTokenParaLogin(loginODP.Login.ToUpper());

                    return new LoginODN()
                    {
                        Login = loginODP.Login,
                        Senha = this.DesCriptografar(loginODP.Senha)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw new DominioExcecao(ex.Message);
            }
        }

        protected internal override void AntesDeSalvar(LoginODP obj, bool inserindo)
        {
            obj.Senha = Criptografar(obj.Senha);

            if (inserindo)
            {
                obj.CodLogin = this.repositorio.ObterProximoValorDeSequencia("TD_LOGIN_SEQ");
            }
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var codLogin = (obj as LoginODN).CodLogin;

            if (codLogin == 0)
            {
                return null;
            }

            return new object[] { codLogin };
        }
        
        public string GerarTokenParaLogin(string login)
        {
            try
            {
                var loginOdn = this.ObterLoginPorLogin(login);

                if (loginOdn == null)
                {
                    throw new ErroDeValidacaoDE($"Login {login} não existe");
                }

                LoginInfoODP loginInfo = this.loginInfoRep.ObterLoginInfoPorLogin(login);

                // Inativa tokens inválidos
                if (loginInfo != null && (DateTime.Now.Subtract(loginInfo.DataCad)).TotalMinutes > this.ObterTokenTempoDeVida())
                {
                    loginInfo.Ativo = 0;
                    loginInfo.DataAlt = DateTime.Now;
                    loginInfo.UsuarioAlt = "SISTEMA";

                    this.loginInfoRep.Salvar(loginInfo);

                    loginInfo = null;
                }

                if (loginInfo == null)
                {
                    loginInfo = new LoginInfoODP()
                    {
                        CodLoginInfo = loginInfoRep.ObterProximoValorDeSequencia("TD_LOGININFO_SEQ"),
                        Ativo = 1,
                        DataCad = DateTime.Now,
                        UsuarioCad = login,
                        DataLogin = DateTime.Now,
                        Login = login,

                        DataLogout = null
                    };

                    loginInfo.Token = this.GerarToken(loginInfo);

                    this.loginInfoRep.Inserir(loginInfo);
                }

                return loginInfo.Token;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;                                                                                                                                                                                                                                                                                                                                                                                                                                                  
                }

                throw new DominioExcecao(ex.Message);
            }
        }

        /// <summary>
        /// Tempo de expiração de um token de login em minutos
        /// </summary>
        /// <returns></returns>
        private int ObterTokenTempoDeVida()
        {
            return 480;
        }
    }
}
