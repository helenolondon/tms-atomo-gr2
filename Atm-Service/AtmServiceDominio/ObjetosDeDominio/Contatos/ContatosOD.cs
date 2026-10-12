using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeNegocio;
using AtmServiceDominio.ODN;
using AtmServiceDominio.Utils;
using AtmServiceInfra;
using AtmServiceInfra.Contatos;
using AtmServiceInfra.Erros;
using AtmServiceInfra.ODP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Contatos
{
    public class ContatosOD : AtmObjetoDominio<ContatoODP>, IContatosOD
    {
        IContatosRepositorio contatosRep;

        internal ContatosOD(IRepositorio<ContatoODP> repositorio)
            : base(repositorio)
        {
            this.contatosRep = repositorio as IContatosRepositorio;
        }

        /// <summary>
        /// Retorna lista de usuários.Ordem inicial por nome.
        /// </summary>
        /// <returns></returns>
        public List<ContatoODN> ObterUsuarios()
        {
            var temp = this.contatosRep.ObterContatosPorNome(null);
            var ret = new List<ContatoODN>();

            if (temp != null && temp.Count > 0)
            {
                return ret.FromListaODP(temp);
            }

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

        protected override object[] ObterID(ObjetoDeNegocio obj)
        {
            var temp = obj as ContatoODN;

            if (temp.CodContato <= 0)
            {
                return null;
            }

            return new object[] { temp.CodContato };
        }

        public List<ContatoODN> ObterContatos()
        {
            var temp = this.contatosRep.ObterContatosPorNome(null);
            var ret = new List<ContatoODN>();

            if (temp != null && temp.Count > 0)
            {
                return ret.FromListaODP(temp);
            }

            return null;
        }
        protected internal override void AntesDeSalvar(ContatoODP obj, bool inserindo)
        {
            base.AntesDeSalvar(obj, inserindo);

            if (inserindo)
            {
                obj.CodContato = this.repositorio.ObterProximoValorDeSequencia("TD_CONTATOS_SEQ");
            }
        }
    }
}
