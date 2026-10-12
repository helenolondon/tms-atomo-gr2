using System;
using System.Collections.Generic;
using System.Linq;
using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeDominio.Seguranca;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceDominio.Utils;
using AtmServiceInfra;
using AtmServiceInfra.Erros;
using AtmServiceInfra.ODP;

namespace AtmServiceDominio.ObjetosDeDominio.Usuarios
{
    public class UsuariosOD: AtmObjetoDominio<UsuarioODP>, IUsuariosOD
    {
        const string USU_SYSADM = "ADMIN";
        const string USU_SISTEMA = "SISTEMA";

        IUsuariosRepositorio usuariosRep;

        internal UsuariosOD(IRepositorio<UsuarioODP> repositorio) 
            :base(repositorio)
        {
            this.usuariosRep = repositorio as IUsuariosRepositorio;
        }

        /// <summary>
        /// Retorna lista de usuários.Ordem inicial por nome.
        /// </summary>
        /// <returns></returns>
        public List<UsuarioODN> ObterUsuarios(string nome, string email, string login, string telefone)
        {
            var temp = this.usuariosRep.ObterUsuarios(nome, email, login, telefone);
            var ret = new List<UsuarioODN>();


            if (temp != null && temp.Count > 0)
            {
                temp.RemoveAll(usuario => usuario.Login.ToUpper() == USU_SYSADM || usuario.Login.ToUpper() == USU_SISTEMA);
                return ret.FromListaODP(temp);
            };

            return null;
        }

        /// <summary>
        /// Retorna lista de usuários.Ordem inicial por código.
        /// </summary>
        /// <returns></returns>
        public UsuarioODN ObterUsuarioPorCodigo(int codigoUsuario)
        {
            var odp = this.repositorio.Obter(codigoUsuario);

            if (odp != null)
            {
                var temp = new UsuarioODN();
                temp.FromODP(odp);

                return temp;
            }

            return null;
        }

        /// <summary>
        /// Retorna usuário por Login
        /// </summary>
        /// <returns></returns>
        public UsuarioODN ObterUsuarioPorLogin(string login)
        {
            try
            {
                var odp = this.usuariosRep.ObterUsuarioPorLogin(login);

                if (odp != null)
                {
                    var temp = new UsuarioODN();
                    temp.FromODP(odp);

                    return temp;
                }

                return null;
            }
            catch (MaisDeUmaOcorrenciaEncontradaPE)
            {
                throw new DominioExcecao($"Existe mais de um usuário com o Login {login}");
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                throw new DominioExcecao(e.Message);
            }
        }

        protected internal override void AntesDeSalvar(UsuarioODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            var usu = this.ObterUsuarioPorLogin(obj.Login);

            if (usu != null && (inserindo || usu.CodUsuario != obj.CodUsuario))
            {
                throw new ErroDeValidacaoDE($"Login '{obj.Login}' não está disponível");
            }

            if (inserindo)
            {
                obj.CodUsuario = this.repositorio.ObterProximoValorDeSequencia("TD_USUARIOS_SEQ");
            }
        }

        internal protected override void AntesDeExcluir(UsuarioODP odp)
        {
            if (this.UsuarioSysAdmin(odp) || this.UsuarioSistema(odp))
            {
                throw new OperacaoNaoPermitidaDE($"O usuário {odp.Login} é usuário do sistema e não pode ser excluído");
            }
        }

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var temp = obj as UsuarioODN;

            if (temp.CodUsuario <= 0)
            {
                return null;
            }

            return new object[] { temp.CodUsuario };
        }

        private bool UsuarioSistema(UsuarioODP usuarioOdp)
        {
            return usuarioOdp != null && usuarioOdp.Login.ToUpper() == USU_SISTEMA;
        }

        private bool UsuarioSysAdmin(UsuarioODP usuarioOdp)
        {
            return usuarioOdp != null && usuarioOdp.Login.ToUpper() == USU_SYSADM;
        }
    }
}
